/*
Helper.cs by mtripg6666tdr
* https://github.com/mtripg6666tdr
* https://scrpg.tyanoyu.net/?ref=helper_cs

Distributed under the license: GPL v3.0

Copyright (c) 2016-2020 mtripg6666tdr All Rights Reserved.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

static class Helper
{
    /// <summary>
    /// 絶対パスを返します。
    /// </summary>
    /// <param name="path">相対パス</param>
    /// <returns>絶対パス</returns>
    public static string GetFullPath(string path)
    {
        string ret = $@"{System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')}\{path}";
        return ret;
    }

    public static string GetExceptionString(Exception ex)
    {
        string ret = $"Threw a exception:{ex.Message}. StackTrace:{ex.StackTrace}\r\n";
        return ret;
    }

    /// <summary>
    /// 改行記号を統一します。
    /// </summary>
    /// <param name="NewLineSign">改行記号を指定します</param>
    /// <returns>改行記号がパラメーターNewLineSignに指定された改行記号に統一されます</returns>
    /// <see cref="NewLineSign"/>
    /// <example>"Hello,\r\nWorld.".UnificateNewLineSign(NewLineSign.RN)</example>
    public static string UnificateNewLineSine(this string str, string NewLineSign)
    {
        return str.Replace("\n\r", "\r\n").Replace("\r\n", "\r").Replace("\r", "\n").Replace("\n", NewLineSign);
    }
}
