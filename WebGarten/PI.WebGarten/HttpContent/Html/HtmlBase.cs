namespace PI.WebGarten.HttpContent.Html
{
    using System;

    using PI.WebGarten.Html;

    public class HtmlBase
    {
        public static IWritable Text(String s) { return new HtmlText(s); }
        public static IWritable H1(params IWritable[] c) { return new HtmlElem("h1", c); }
        public static IWritable H2(params IWritable[] c) { return new HtmlElem("h2", c); }
        public static IWritable H3(params IWritable[] c) { return new HtmlElem("h3", c); }
        public static IWritable Form(String method, String url, params IWritable[] c)
        {
            return new HtmlElem("form", c)
                .WithAttr("method", method)
                .WithAttr("action", url);
        }
        public static IWritable Label(String to, String text)
        {
            return new HtmlElem("label", new HtmlText(text))
                .WithAttr("for", to);
        }

        public static IWritable InputText(String name)
        {
            return new HtmlElem("input")
                .WithAttr("type", "text")
                .WithAttr("name", name);
        }

        //metodo adicionado
        public static IWritable InputTextWithContent(String name, String content)
        {
            return new HtmlElem("input")
                .WithAttr("type", "text")
                .WithAttr("name", name)
                .WithAttr("value", content);
        }

        //metodo adicionado
        public static IWritable InputCheckBox(String name,String check)
        {
            return new HtmlElem("input")
                .WithAttr("type", "checkbox")
                .WithAttr("name", name)
                .WithAttr("checked",check);
        }

        //metodo adicionado
        public static IWritable InputTextArea(String name, String rows, String cols)
        {
            return new HtmlElem("textarea")
                .WithAttr("rows", rows)
                .WithAttr("cols", cols)
                .WithAttr("name", name);
        }

        public static IWritable InputTextArea(String name, String rows, String cols,string content)
        {
            return new HtmlElem("textarea")
            .WithAttr("value", content)
                .WithAttr("rows", rows)
                .WithAttr("cols", cols)
                .WithContent(Text(content));
                
        }

        //metodo adicionado
        public static IWritable InputFieldset(params IWritable[] cs)
        {
            return new HtmlElem("fieldset", cs);
        }

        //metodo adicionado
        public static IWritable InputLegend(string name)
        {
            return new HtmlElem("legend")
                .WithContent(Text(name));
        }

        public static IWritable InputSubmit(String value)
        {
            return new HtmlElem("input")
                .WithAttr("type", "submit")
                .WithAttr("value", value);
        }
        public static IWritable Ul(params IWritable[] c)
        {
            return new HtmlElem("ul", c);
        }
        public static IWritable Li(params IWritable[] c)
        {
            return new HtmlElem("li", c);
        }
        public static IWritable P(params IWritable[] c)
        {
            return new HtmlElem("p", c);
        }
        public static IWritable A(String href, String t)
        {
            return new HtmlElem("a", Text(t))
                .WithAttr("href", href);
        }
        public static IWritable Img(string src, string alt)
        {
            return new HtmlElem("img")
                .WithAttr("src", src)
                .WithAttr("alt", alt);
        }
    }
}