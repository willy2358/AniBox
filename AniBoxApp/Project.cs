using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using AniBox.Framework.Region;
using System.Reflection;
using AniBox.Framework.Controls;

namespace AniBox
{
    public class Project
    {
        private string _projectFile = "";
        private List<AniRegion> _regions = new List<AniRegion>();

        public static Project LoadProject(string prjFile)
        {
            try
            {
                Project project = new Project();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(prjFile);

                XmlNode nodeRegions = xmlDoc.SelectSingleNode("./Regions");
                XmlNodeList regions = nodeRegions.SelectNodes("./Region");
                for(int i = 0; i < regions.Count; i++)
                {
                    XmlNode nodeRegion = regions[i];

                    AniRegion region = LoadRegion(nodeRegion);
                    
                    LoadRegionControls(nodeRegion, region);
                }

                return project;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        private static AniRegion LoadRegion(XmlNode nodeRegion)
        {
            string regionName = nodeRegion.Attributes["TypeName"].Value;
            string assemblyName = nodeRegion.Attributes["Assembly"].Value;
            Type type = FindType(regionName);
            
            if (null == type)
            {
                return null ;
            }
            AniRegion region = Activator.CreateInstance(type) as AniRegion;
            foreach(XmlAttribute attr in nodeRegion.Attributes)
            {
                PropertyInfo propInfo = type.GetProperty(attr.Name);
                if (null != propInfo)
                {
                    if (propInfo.PropertyType == typeof(int))
                    {
                        propInfo.SetValue(region, Convert.ToInt32(attr.Value));
                    }
                    else if (propInfo.PropertyType == typeof(double))
                    {
                        propInfo.SetValue(region, Convert.ToDouble(attr.Value));
                    }
                    else if (propInfo.PropertyType == typeof(string))
                    {
                        propInfo.SetValue(region, attr.Value.ToString());
                    }
                }
            }
            return region;
        }

        private static Type FindType(string type)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach(var a in assemblies)
            {
                Type[] ts = a.GetTypes();
                foreach(Type t in ts)
                {
                    if (t.Name == type)
                    {
                        return t;
                    }
                }
            }

            return null;
        }

        private static void LoadRegionControls(XmlNode nodeRegion, AniRegion regionObj)
        {
            if (null == regionObj)
            {
                return;
            }
            XmlNodeList nodeControls = nodeRegion.SelectNodes("./Controls/Control");
            for(int i = 0; i < nodeControls.Count; i++)
            {
                XmlNode nodeControl = nodeControls[i];
                AniControl control = LoadControl(nodeControl);
                if (null != control)
                {
                    regionObj.AniControls.Add(control);
                }
            }
        }

        private static AniControl LoadControl(XmlNode nodeControl)
        {
            string controlName = nodeControl.Attributes["TypeName"].Value;
            string assemblyName = nodeControl.Attributes["Assembly"].Value;
            Type type = FindType(controlName);
            if (null == type)
            {
                return null;
            }
            AniControl control = Activator.CreateInstance(type) as AniControl;
            foreach (XmlAttribute attr in nodeControl.Attributes)
            {
                PropertyInfo propInfo = type.GetProperty(attr.Name);
                if (null != propInfo)
                {
                    if (propInfo.PropertyType == typeof(int))
                    {
                        propInfo.SetValue(control, Convert.ToInt32(attr.Value));
                    }
                    else if (propInfo.PropertyType == typeof(double))
                    {
                        propInfo.SetValue(control, Convert.ToDouble(attr.Value));
                    }
                    else if (propInfo.PropertyType == typeof(string))
                    {
                        propInfo.SetValue(control, attr.Value.ToString());
                    }
                }
            }
            return control;
        }

        public void AddRegion(AniRegion region)
        {
            _regions.Add(region);
        }

        public void InsertRegion(int pos, AniRegion region)
        {
            _regions.Insert(pos, region);
        }

        public bool SaveAs(String projFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclare = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            xmlDoc.AppendChild(xmlDeclare);
            XmlNode nodeRegions = xmlDoc.AppendChild(xmlDoc.CreateElement("Regions"));
            for (int i = 0; i < _regions.Count; i++ )
            {
                AddRegionXmlNode(nodeRegions, _regions[i]);
            }

            try
            {
                xmlDoc.Save(projFile);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

        private void AddRegionXmlNode(XmlNode nodeRegions, AniRegion region)
        {
            XmlNode nodeRegion = nodeRegions.OwnerDocument.CreateElement("Region");
            nodeRegions.AppendChild(nodeRegion);

            SaveRegionProperties(region, nodeRegion);

            SaveRegionControls(region, nodeRegion);
        }

        private static void SaveRegionControls(AniRegion region, XmlNode nodeRegion)
        {
            XmlNode nodeControls = nodeRegion.OwnerDocument.CreateElement("Controls");
            nodeRegion.AppendChild(nodeControls);

            foreach (AniControl control in region.AniControls)
            {
                XmlNode nodeControl = nodeControls.OwnerDocument.CreateElement("Control");

                SaveControlProperties(control, nodeControl);

                nodeControls.AppendChild(nodeControl);
            }
        }

        private static void SaveControlProperties(AniControl control, XmlNode nodeControl)
        {
            XmlAttribute xmlAttrAssembly = nodeControl.OwnerDocument.CreateAttribute("Assembly");
            xmlAttrAssembly.Value = control.GetType().Assembly.FullName;
            nodeControl.Attributes.Append(xmlAttrAssembly);

            XmlAttribute xmlAttrTypeName = nodeControl.OwnerDocument.CreateAttribute("TypeName");
            xmlAttrTypeName.Value = control.GetType().Name;
            nodeControl.Attributes.Append(xmlAttrTypeName);

            foreach (PropertyInfo p in control.GetType().GetProperties())
            {
                if (null != p.GetCustomAttribute(typeof(AniBox.Framework.Attributes.AniPropertyAttribute), true))
                {
                    XmlAttribute xmlAttr = nodeControl.OwnerDocument.CreateAttribute(p.Name);
                    Object prop = p.GetValue(control);
                    if (null != prop)
                    {
                        xmlAttr.Value = prop.ToString();
                    }
                    else
                    {
                        xmlAttr.Value = "";
                    }
                    nodeControl.Attributes.Append(xmlAttr);
                }
            }
        }

        private static void SaveRegionProperties(AniRegion region, XmlNode nodeRegion)
        {
            XmlAttribute xmlAttrAssembly = nodeRegion.OwnerDocument.CreateAttribute("Assembly");
            xmlAttrAssembly.Value = region.GetType().Assembly.FullName;
            nodeRegion.Attributes.Append(xmlAttrAssembly);

            XmlAttribute xmlAttrTypeName = nodeRegion.OwnerDocument.CreateAttribute("TypeName");
            xmlAttrTypeName.Value = region.GetType().Name;
            nodeRegion.Attributes.Append(xmlAttrTypeName);

            foreach (PropertyInfo p in region.GetType().GetProperties())
            {
                if (null != p.GetCustomAttribute(typeof(AniBox.Framework.Attributes.AniPropertyAttribute), true))
                {
                    XmlAttribute xmlAttr = nodeRegion.OwnerDocument.CreateAttribute(p.Name);
                    Object prop = p.GetValue(region);
                    if (null != prop)
                    {
                        xmlAttr.Value = prop.ToString();
                    }
                    else
                    {
                        xmlAttr.Value = "";
                    }
                    nodeRegion.Attributes.Append(xmlAttr);
                }
            }
        }
    }
}
