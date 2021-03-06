﻿using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ImdbCrawlerDesktop
{
    /// <summary>
    /// Código para mudar a cor da progress bar
    /// </summary>
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }

    public class Utils
    {

    }

    public enum ChartType
    {
        MOVIES_BY_RATING, MOVIE_BY_GENRE, ACTORS_AWARDED, DIRECTORS_AWARDED
    }

    public enum ActionType
    {
        FIND_ACTORS, FIND_DIRECTORS
    }
}
