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
    public partial class Principal : Form
    {
        #region Propiedades
        readonly private int _Width = 800;
        readonly private int _Height = 600;

        #endregion

        public Principal()
        {
            InitializeComponent();
            // IsMdiContainer = true; // Lo convertimos a container
            MinimumSize = new Size(_Width, _Height); // Damos tamaño minimo
            MaximizeBox = false; // Deshabilitamos maximizar ventana
            ShowIcon = false; // Ocultamos icono
            StartPosition = FormStartPosition.CenterScreen; // Posicion de inicio
            BackColor = Color.White;
            Text = "Sistema Administración de Hotel";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Pintamos las habitaciones
            PrintRooms();
        }

        #region PrintRooms
        /// <summary>
        /// Creamos las habitaciones
        /// </summary>
        private void PrintRooms()
        {
            int x = 0, y = 70, w = 90, h = 90;

            // Obtenemos las habitaciones
            var Rooms = new RoomMapper().GetRooms();
            // Creamos la botonera
            Button[] Btns = new Button[Rooms.Count()];

            // Creamos los botones
            int i = 0;
            foreach(var Room in Rooms)
            {
                // Obtenemos la posicion de X y Y
                if (x == 0) x = 50;
                else if ((x + w + 10) < (Width - w - 10)) x = x + w + 10;
                else
                {
                    x = 50;
                    y = y + w + 10;
                }

                // Creamos el boton
                Btns[i] = new Button();

                // Asignamos las propiedades al boton
                Btns[i].Text = Room.name;
                Btns[i].Click += new EventHandler(ButtonClick);
                Btns[i].BackColor = ColorTranslator.FromHtml(Utils.GetColor((int)Room.id_status));
                Btns[i].ForeColor = ColorTranslator.FromHtml("#ffffff");
                Btns[i].FlatAppearance.BorderColor = ColorTranslator.FromHtml("#e0e0e0");
                Btns[i].SetBounds(x, y, w, h);
                Btns[i].Tag = Room; // Pasamos la información

                // Agregamos el boton al formulario
                Controls.Add(Btns[i]);

                i++; // aumentamos el control de indice
            }
        }
        #endregion

        #region ButtonClick
        /// <summary>
        /// Evento que se dispara al hacer click a algun boton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            try
            {
                // Obtenemos el boton que envia
                Button btn = (Button) sender;
                var room = (RoomsModel) btn.Tag;

                // Calculamos si ya existe una reservacion
                if (room.id_status == 1 || room.id_status == 2)
                {
                    // La habitacion tiene o esta lista para reservar
                    frmHabitacion FrmH = new frmHabitacion(btn);
                    DialogResult dr = FrmH.ShowDialog(this);
                    if (dr == DialogResult.Cancel)
                    {
                        // FrmH.Close();
                    }
                    else if (dr == DialogResult.OK)
                    {
                        // FrmH.Close();
                    }
                }
                else
                {
                    // La habitacion no esta lista para reservar
                    frmStatus FrmS = new frmStatus(btn);
                    DialogResult dr = FrmS.ShowDialog(this);
                    if (dr == DialogResult.Cancel)
                    {
                        // FrmH.Close();
                    }
                    else if (dr == DialogResult.OK)
                    {
                        // FrmH.Close();
                    }
                }
            }
            catch(Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
        }
        #endregion
    }
}
