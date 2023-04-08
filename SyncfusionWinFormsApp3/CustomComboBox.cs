//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SyncfusionWinFormsApp3;

//public class CustomComboBox : ComboBox
//    {
//        protected override void OnPaint(PaintEventArgs e)
//        {
//            base.OnPaint(e);

//            if (this.SelectedIndex < 0)
//            {
//                return;
//            }

//            Users user = (Users)this.SelectedItem;
//            string defaultUsername = System.Configuration.ConfigurationManager.AppSettings["DefaultUsername"];
//            Brush brush = (user.Username == defaultUsername) ? Brushes.DarkMagenta : Brushes.Black;

//            e.Graphics.DrawString(user.ToString(), this.Font, brush, new PointF(3, 3));
//        }

//        protected override void OnSelectedIndexChanged(EventArgs e)
//        {
//            base.OnSelectedIndexChanged(e);
//            this.Invalidate(); // Force the control to repaint when the selected index changes
//        }
//    }


