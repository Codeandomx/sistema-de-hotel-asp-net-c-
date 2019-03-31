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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_hotel
{
    public partial class frmStatus : Form
    {
        protected Button _btn;
        RoomsModel _room;

        public frmStatus(Button btn)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterParent; // Posicion de inicio

            // Obtenemos los datos
            _btn = btn;
            _room = (RoomsModel)btn.Tag;

            // Validamos el status
            switch (_room.id_status)
            {
                case 3:
                    btnServicio.Text = "Seguir en Servicio";
                    lblStatus.Text = "En Servicio";
                    break;
                case 4:
                    btnMantenimiento.Text = "Seguir en Mantenimiento";
                    lblStatus.Text = "En Mantenimiento";
                    break;
            }

            // Asignamos color
            lblStatus.BackColor = ColorTranslator.FromHtml(Utils.GetColor((int)_room.id_status));
        }

        private void btnServicio_Click(object sender, EventArgs e)
        {
            // Validamos si es el estado actual
            if(_room.id_status != 3)
            {
                // Asignamos el nuevo estado
                _room.id_status = 3;

                // Actualizamos la habitación
                int id = new RoomMapper().EditRoom(_room);

                if (id > 0)
                {
                    MessageBox.Show(
                        "La habitación se encuentra en servicio",
                        "Información de Habitación",
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
                // Si es el estado actual solo cerramos
                Dispose();
            }
        }

        private void btnMantenimiento_Click(object sender, EventArgs e)
        {
            // Validamos si es el estado actual
            if (_room.id_status != 4)
            {
                // Asignamos el nuevo estado
                _room.id_status = 4;

                // Actualizamos la habitación
                int id = new RoomMapper().EditRoom(_room);

                if (id > 0)
                {
                    MessageBox.Show(
                        "La habitación se encuentra en mantenimiento",
                        "Información de Habitación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Actualizamos el boton
                    _btn.BackColor = ColorTranslator.FromHtml(Utils.GetColor(4));
                    _btn.Tag = _room;
                }

                // Cerramos la ventana
                Dispose();
            }
            else
            {
                // Si es el estado actual solo cerramos
                Dispose();
            }
        }

        private void btnDisponible_Click(object sender, EventArgs e)
        {
            // Asignamos el nuevo estado
            _room.id_status = 1;

            // Actualizamos la habitación
            int id = new RoomMapper().EditRoom(_room);

            if (id > 0)
            {
                MessageBox.Show(
                    "La habitación se encuentra disponible",
                    "Información de Habitación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // Actualizamos el boton
                _btn.BackColor = ColorTranslator.FromHtml(Utils.GetColor(1));
                _btn.Tag = _room;
            }

            // Cerramos la ventana
            Dispose();
        }
    }
}
