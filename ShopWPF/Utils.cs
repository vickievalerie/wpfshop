using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace WPFShop
{
    internal class Utils
    {
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        public static XElement GetXMLElement(XElement XML, string ElemName, string IdName, string IdValue)
        {
            return (from x in XML.Elements(ElemName) where x.Element(IdName).Value == IdValue select x).FirstOrDefault();
        }

        public static string GetXMLField(XElement XML, string ElemName, string IdName, string IdValue, string FieldName)
        {
            var x = GetXMLElement(XML, ElemName, IdName, IdValue);
            return x.Element(FieldName).Value;
        }
    }
}
