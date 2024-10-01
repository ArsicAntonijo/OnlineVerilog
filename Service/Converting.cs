namespace OnlineVerilog.Service
{
    public static class Converting
    {
        public static string HtmlToString(string html) 
        {
            html = html.Replace("<br>", "\r\n");
            html = html.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "\t");
            return html;
        }
        public static string StringToHtml(string str)
        {
            str = str.Replace("\r\n", "<br>");
            str = str.Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;");
            return str;
        }
    }
}
