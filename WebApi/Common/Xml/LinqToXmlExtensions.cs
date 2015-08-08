#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#endregion

namespace ThatConference.Common.Xml
{
    /// <summary>
    /// This class defines common methods used in Linq to XML.
    /// </summary>
    public static class LinqToXmlExtensions
    {

        /// <summary>
        /// Returns a collection of the descendant elements for this document or element, in document order and 
        /// ignoring case.
        /// </summary>
        /// <param name="container">The class being extended.</param>
        /// <param name="name">The name of the element.</param>
        /// <returns>IEnumerable of XElement.</returns>
        public static IEnumerable<XElement> DescendantsIgnoreCase(this XContainer container, XName name)
        {
            foreach (XElement element in container.Descendants())
            {
                if ((element.Name.NamespaceName == name.NamespaceName ||
                     element.Name.NamespaceName.Equals(name.NamespaceName, StringComparison.OrdinalIgnoreCase)) &&
                    String.Equals(element.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Returns a collection of the descendant elements for this document or element, in document order, 
        /// ignoring case.
        /// </summary>
        /// <param name="container">The class being extended.</param>
        /// <param name="name">The name of the element.</param>
        /// <returns>IEnumerable of XElement.</returns>
        public static IEnumerable<XElement> DescendantsIgnoreCase(this IEnumerable<XElement> container, XName name)
        {
            foreach (XElement element in container.Descendants())
            {
                if ((element.Name.NamespaceName == name.NamespaceName ||
                     element.Name.NamespaceName.Equals(name.NamespaceName, StringComparison.OrdinalIgnoreCase)) &&
                    String.Equals(element.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Returns a collection of the child elements of every element and document in the source collection, 
        /// ignoring case.
        /// </summary>
        /// <param name="container">The class being extended.</param>
        /// <param name="name">The name of the element.</param>
        /// <returns>IEnumerable of XElement.</returns>
        public static IEnumerable<XElement> ElementsIgnoreCase(this IEnumerable<XElement> container, XName name)
        {
            foreach (XElement element in container.Elements())
            {
                if ((element.Name.NamespaceName == name.NamespaceName ||
                     element.Name.NamespaceName.Equals(name.NamespaceName, StringComparison.OrdinalIgnoreCase)) &&
                    String.Equals(element.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Returns a filtered collection of the child elements of this element or document,
        ///     in document order. Only elements that have a matching System.Xml.Linq.XName
        ///     are included in the collection, ignoring case.
        /// </summary>
        /// <param name="container">The class being extended.</param>
        /// <param name="name">The name of the element.</param>
        /// <returns>IEnumerable of XElement.</returns>
        public static IEnumerable<XElement> ElementsIgnoreCase(this XContainer container, XName name)
        {
            foreach (XElement element in container.Elements())
            {
                if ((element.Name.NamespaceName == name.NamespaceName ||
                     element.Name.NamespaceName.Equals(name.NamespaceName, StringComparison.OrdinalIgnoreCase)) &&
                    String.Equals(element.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return element;
                }
            }
        }

        /// <summary>
        /// Gets the first (in document order) child element with the specified System.Xml.Linq.XName, 
        /// ignoring case.
        /// </summary>
        /// <param name="element">The class being extended.</param>
        /// <param name="name">The name of the element.</param>
        /// <returns>XElement.</returns>
        public static XElement ElementIgnoreCase(this XElement element, XName name)
        {
            var el = element.Element(name);
            if (el != null)
                return el;

            return
                element.Elements()
                    .FirstOrDefault(
                        s => string.Equals(s.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        ///  Returns a filtered collection of attributes of this element. Only elements that have a 
        /// matching System.Xml.Linq.XName are included in the collection, ignoring case.
        /// </summary>
        /// <param name="element">The class being extended.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>IEnumerable of XAttribute.</returns>
        public static IEnumerable<XAttribute> AttributesIgnoreCase(this XElement element, XName name)
        {
            foreach (XAttribute attr in element.Attributes())
            {
                if ((attr.Name.NamespaceName == name.NamespaceName ||
                     attr.Name.NamespaceName.Equals(name.NamespaceName, StringComparison.OrdinalIgnoreCase)) &&
                    String.Equals(attr.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase))
                {
                    yield return attr;
                }
            }
        }

        /// <summary>
        /// Returns a filtered collection of attributes of this element. Only elements
        ///     that have a matching System.Xml.Linq.XName are included in the collection, ignoring case.
        /// </summary>
        /// <param name="element">The class being extended.</param>
        /// <param name="name">The name of the attribute</param>
        /// <returns>XAttribute</returns>
        public static XAttribute AttributeIgnoreCase(this XElement element, XName name)
        {
            return
                element.Attributes()
                    .FirstOrDefault(
                        s => string.Equals(s.Name.LocalName, name.LocalName, StringComparison.OrdinalIgnoreCase));
        }

    }
}