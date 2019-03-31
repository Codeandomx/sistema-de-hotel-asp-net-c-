using Sistema_de_hotel.Entities;
using Sistema_de_hotel.helpers;
using Sistema_de_hotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_hotel
{
    public partial class frmHabitacion : Form
    {
        protected Button _btn;
        ReservationsModel _reservation;
        RoomsModel _room;
        protected char _SignoDecimal; // Carácter separador decimal
        protected string _PrevTextBoxPrice; // Variable que almacena el valor anterior del Textbox

        public frmHabitacion(Button btn)
        {
            InitializeComponent();
            _PrevTextBoxPrice = string.Empty;
            _SignoDecimal = '.';

            StartPosition = FormStartPosition.CenterParent; // Posicion de inicio

            // Obtenemos los datos
            _btn = btn;
            _room = (RoomsModel) btn.Tag;

            // Validamos el status
            if (_room.id_status == 2)
            {
                _reservation = new ReservationMapper().GetReservation((int)_room.id_room);

                // Actualizamos el campo status name
                _room.status_name = _reservation.status_name;

                // Llenamos los campos
                txtPrice.Text = _reservation.price.ToString();
                txtName.Text = _reservation.name;

                // Obtenemos los dias transcurridos
                DateTime register = (DateTime)_reservation.date_register;
                int Days = (int)(DateTime.Now - register).TotalDays;

                // Calculamos el precio
                txtTotal.Text = (_reservation.price * (Days + 1)).ToString();
            }
            else
            {
                // Calculamos el precio
                txtTotal.Text = "0.0";
                btnCobrar.Text = "Reservar";
            }

            // Llenamos los campos
            lblStatus.Text = _room.status_name;
            lblType.Text = _room.type_name;
            Text = Text + " "+ _room.name;
            lblHabitacion.Text = lblHabitacion.Text + " " + _room.name;

            // Coloreamos el status
            lblStatus.BackColor = ColorTranslator.FromHtml(Utils.GetColor((int)_room.id_status));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Cerramos la ventana
            Dispose();
        }

        #region Cobrar
        private void btnCobrar_Click(object sender, EventArgs e)
        {
            try
            {
                #region Validaciones
                // Validamos los campos
                if(txtName.Text.Length <= 5)
                {
                    MessageBox.Show(
                        "Error el nombre debe tener al menos 6 caracteres",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }
                else if(txtPrice.Text.Length == 0)
                {
                    MessageBox.Show(
                            "Error el precio no es valido",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                    return;
                } else if (Convert.ToDecimal(txtPrice.Text) < 1)
                {
                    MessageBox.Show(
                            "Error el precio no es valido",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                    return;
                }
                #endregion

                // Validamos el status
                if (_room.id_status == 1)
                {
                    // Creamos la reservación
                    int id = new ReservationMapper().CreateReservation(
                            (int)_room.id_room,
                            txtName.Text,
                            Convert.ToDouble(txtPrice.Text)
                        );

                    // Validamos la creación de la reservacion
                    if (id > 0)
                    {
                        _room.id_status = 2;

                        // Actualizamos la habitación
                        id = new RoomMapper().EditRoom(_room);

                        if (id > 0)
                        {
                            MessageBox.Show(
                                "La reservación se creo con exito",
                                "Información de reservación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            // Actualizamos el boton
                            _btn.BackColor = ColorTranslator.FromHtml(Utils.GetColor(2));
                            _btn.Tag = _room;
                        }

                        // Cerramos la ventana
                        Dispose();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Error al crear la reservación intente de nuevo",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                else
                {
                    // Actualizamos la reservacion
                    _reservation.name = txtName.Text;
                    _reservation.price = Convert.ToDouble(txtPrice.Text);
                    _reservation.total = Convert.ToDouble(txtTotal.Text);
                    _reservation.active = false;
                    int id = new ReservationMapper().EditReservation(
                            _reservation
                        );

                    // Validamos la actualizacion de la reservacion
                    if (id > 0)
                    {
                        _room.id_status = 3;
                        _room.status_name = "En Servicio";

                        // Actualizamos la habitación
                        id = new RoomMapper().EditRoom(_room);

                        if (id > 0)
                        {
                            MessageBox.Show(
                                "La habitación se encuentra en limpieza",
                                "Información de reservación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );

                            // Actualizamos el boton
                            _btn.BackColor = ColorTranslator.FromHtml(Utils.GetColor(3));
                            _btn.Tag = _room;
                        }

                        // Cerramos la ventana
                        Dispose();
                    }
                    else
                    {
                        MessageBox.Show(
                            "Error al finalizar la reservación intente de nuevo",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }

            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
        #endregion

        #region CalculamosTotalYRedondeamos
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            // Redondeamos el price
            txtPrice.Text = (decimal.Round(Convert.ToDecimal(txtPrice.Text), 2)).ToString();

            // Calculamos el total
            if(_room.id_status == 1)
            {
                txtTotal.Text = txtPrice.Text;
            } 
            else
            {
                // Obtenemos los dias transcurridos
                DateTime register = (DateTime)_reservation.date_register;
                int Days = (int)(DateTime.Now - register).TotalDays;

                // Calculamos el precio
                txtTotal.Text = (decimal.Round(Convert.ToDecimal(txtPrice.Text) * (Days + 1),2)).ToString();
            }
        }
        #endregion

        #region NumerosDecimales
        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;
            // Comprueba si el valor del TextBox se ajusta a un valor válido
            if (Regex.IsMatch(textBox.Text, @"^(?:\d+\.?\d*)?$"))
            {
                // Si es válido se almacena el valor actual en la variable privada
                _PrevTextBoxPrice = textBox.Text;
            }
            else
            {
                // Si no es válido se recupera el valor de la variable privada con el valor anterior
                // Calcula el nº de caracteres después del cursor para dejar el cursor en la misma posición
                var charsAfterCursor = textBox.TextLength - textBox.SelectionStart - textBox.SelectionLength;
                // Recupera el valor anterior
                textBox.Text = _PrevTextBoxPrice;
                // Posiciona el cursor en la misma posición
                textBox.SelectionStart = Math.Max(0, textBox.TextLength - charsAfterCursor);
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            // Si el carácter pulsado no es un carácter válido se anula
            e.Handled = !char.IsDigit(e.KeyChar) // No es dígito
                        && !char.IsControl(e.KeyChar) // No es carácter de control (backspace)
                        && (e.KeyChar != _SignoDecimal // No es signo decimal o es la 1ª posición o ya hay un signo decimal
                            || textBox.SelectionStart == 0
                            || textBox.Text.Contains(_SignoDecimal));
        }
        #endregion
    }
}
