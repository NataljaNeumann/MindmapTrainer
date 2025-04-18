﻿/*  Mindmap-Trainer aims to help people in training for exams
    Copyright (C) 2024-2025 NataljaNeumann@gmx.de

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License along
    with this program; if not, write to the Free Software Foundation, Inc.,
    51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
*/


using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;

namespace MindmapTrainer
{

    //*******************************************************************************************************
    /// <summary>
    /// The main progam
    /// </summary>
    //*******************************************************************************************************
    static class Program
    {
        //===================================================================================================
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        //===================================================================================================
        [STAThread]
        static void Main()
        {
#if DEBUG
#if DEBUG
            // string strSetCulture =
            // "af-ZA";
            // "ar-SA";
            // "az-Latn-AZ";
            // "be-BY";
            // "bg-BG";
            // "bs-Latn-BA";
            // "cs-CZ";
            // "da-DK";
            // "de-DE";
            // "el-GR";
            // "es-ES";
            // "et-EE";
            // "fa-IR";
            // "fi-FI";
            // "fr-FR";
            // "he-IL";
            // "hi-IN";
            // "hu-HU";
            // "hy-AM";
            // "id-ID";
            // "is-IS";
            // "it-IT";
            // "ja-JP";
            // "ka-GE";
            // "kk-KZ";
            // "km-KH";
            // "ko-KR";
            // "ky-KG";
            // "lt-LT";
            // "lv-LV";
            // "mk-MK";
            // "mn-MN";
            // "ms-MY";
            // "nl-NL";
            // "no-NO";
            // "pa-Arab-PK";
            // "pa-IN";
            // "pl-PL";
            // "ps-AF";
            // "pt-PT";
            // "en-US";
            // "ro-RO";
            // "ru-RU";
            // "sa-IN";
            // "sk-SK";
            // "sl-SL";
            // "sr-Cyrl-RS"; // TODO: need a fix
            // "sv-SE";
            // "tg-Cyrl-TJ";
            // "th-TH";
            // "tr-TR";
            // "uk-UA";
            // "uz-Latn-UZ";
            // "vi-VN";
            // "zh-TW";
            // "zh-CN";

            // "ga-IE";
            // "ku-Arab-IQ";
            // "ti-ER";
            // "am-ET";
            // "ig-NG";
            // "yo-NG";
            // "wo-SN";
            // "so-SO";
            // "rw-RW";
            // System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(strSetCulture);
            // System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(strSetCulture);
#endif
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MindmapTrainerForm());
        }
    }
}
