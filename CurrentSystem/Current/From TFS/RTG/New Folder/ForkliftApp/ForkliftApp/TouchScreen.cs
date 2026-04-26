//***************************************************************************
//              Copyright © EffiaSoft Pvt. Ltd.
//                       www.effiasoft.com
//***************************************************************************
//  Developed By: dorababu@effiasoft.com
//  Development Date: 01 July 2013
//  Last Modified By: dorababu@effiasoft.com
//  Last Modified Date: 01 July 2013
//***************************************************************************

using System;
using System.Windows.Forms;

namespace NumericKeyPad
{
    public partial class TouchScreen : UserControl
    {
        public delegate void ButtonClickedEventHandler(object sender, EventArgs e);
        public event ButtonClickedEventHandler OnUserControlButtonClicked;
        public TouchScreen()
        {
            InitializeComponent();
        }

        #region ButtonClick
        /// <summary>
        /// Touch pad button which will respond to the selected operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, EventArgs e)
        {
            Button btnNumber =  sender as Button;
            
            switch (btnNumber.Text)
            {
                case "0":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "1":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "2":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "3":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "4":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "5":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "6":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "7":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "8":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "9":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "*":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "%":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "מחק":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;

                case "A":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "B":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "C":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "D":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;




                case "E":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "F":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "G":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "H":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;





                case "L":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "O":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                case "R":
                    OnUserControlButtonClicked(btnNumber, e);
                    MyGlobal.bTouch = false;
                    break;
                







            
            }
        }
        #endregion





    }
}
