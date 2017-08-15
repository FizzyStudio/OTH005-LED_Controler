using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace CFD_Define
{
    public class XMLConfig
    {
        /// <summary>
        /// 得到程序工作目录
        /// </summary>
        /// <returns></returns>
        private static string GetWorkDirectory()
        {
            try
            {
                return Path.GetDirectoryName(typeof(XMLConfig).Assembly.Location);
            }
            catch
            {
                return System.Windows.Forms.Application.StartupPath;
            }
        }

        /// <summary>
        /// 判断字符串是否为空串
        /// </summary>
        /// <param name="szString">目标字符串</param>
        /// <returns>true:为空串;false:非空串</returns>
        private static bool IsEmptyString(string szString)
        {
            if (szString == null)
                return true;
            if (szString.Trim() == string.Empty)
                return true;
            return false;
        }

        /// <summary>
        /// 创建一个制定根节点名的XML文件
        /// </summary>
        /// <param name="szFileName">XML文件</param>
        /// <param name="szRootName">根节点名</param>
        /// <returns>bool</returns>
        private static bool CreateXmlFile(string szFileName, string szRootName)
        {
            if (szFileName == null || szFileName.Trim() == "")
                return false;
            if (szRootName == null || szRootName.Trim() == "")
                return false;

            XmlDocument clsXmlDoc = new XmlDocument();
            clsXmlDoc.AppendChild(clsXmlDoc.CreateXmlDeclaration("1.0", "GBK", null));
            clsXmlDoc.AppendChild(clsXmlDoc.CreateNode(XmlNodeType.Element, szRootName, ""));
            try
            {
                clsXmlDoc.Save(szFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从XML文件获取对应的XML文档对象
        /// </summary>
        /// <param name="szXmlFile">XML文件</param>
        /// <returns>XML文档对象</returns>
        private static XmlDocument GetXmlDocument(string szXmlFile)
        {
            if (IsEmptyString(szXmlFile))
                return null;
            if (!File.Exists(szXmlFile))
                return null;
            XmlDocument clsXmlDoc = new XmlDocument();
            try
            {
                clsXmlDoc.Load(szXmlFile);
            }
            catch
            {
                return null;
            }
            return clsXmlDoc;
        }

        /// <summary>
        /// 将XML文档对象保存为XML文件
        /// </summary>
        /// <param name="clsXmlDoc">XML文档对象</param>
        /// <param name="szXmlFile">XML文件</param>
        /// <returns>bool:保存结果</returns>
        private static bool SaveXmlDocument(XmlDocument clsXmlDoc, string szXmlFile)
        {
            if (clsXmlDoc == null)
                return false;
            if (IsEmptyString(szXmlFile))
                return false;
            try
            {
                if (File.Exists(szXmlFile))
                    File.Delete(szXmlFile);
            }
            catch
            {
                return false;
            }
            try
            {
                clsXmlDoc.Save(szXmlFile);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///  读取指定的配置文件中指定Key的值
        /// </summary>
        /// <param name="szKeyName">读取的Key名称</param>
        /// <param name="szDefaultValue">指定的Key不存在时,返回的值</param>
        /// <returns>Key值</returns>
        public static string GetConfigData(string file, string szKeyName, string szDefaultValue)
        {
            string szConfigFile = file; //= string.Format("{0}\\{1}", GetWorkDirectory(), CONFIG_FILE);
            if (!File.Exists(szConfigFile))
            {
                return szDefaultValue;
            }

            XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);
            if (clsXmlDoc == null)
                return szDefaultValue;

            string szXPath = "/main/" + szKeyName;//string.Format("/SystemConfig/%s", szKeyName);
            XmlNode clsXmlNode = SelectXmlNode(clsXmlDoc, szXPath);
            if (clsXmlNode == null)
            {
                return szDefaultValue;
            }
            return clsXmlNode.InnerText;
        }

        /// <summary>
        ///  读取指定的配置文件中指定Key的值
        /// </summary>
        /// <param name="szKeyName">读取的Key名称</param>
        /// <param name="szDefaultValue">指定的Key不存在时,返回的值</param>
        /// <returns>Key值</returns>
        public static double GetConfigData(string file, string szKeyName, double nDefaultValue)
        {
            string szValue = GetConfigData(file, szKeyName, nDefaultValue.ToString());
            try
            {
                return double.Parse(szValue);
            }
            catch
            {
                return nDefaultValue;
            }
        }

        public static int GetConfigData(string file, string szKeyName, int nDefaultValue)
        {
            string szValue = GetConfigData(file, szKeyName, nDefaultValue.ToString());
            try
            {
                return int.Parse(szValue);
            }
            catch
            {
                return nDefaultValue;
            }
        }

        /// <summary>
        ///  读取指定的配置文件中指定Key的值
        /// </summary>
        /// <param name="szKeyName">读取的Key名称</param>
        /// <param name="szDefaultValue">指定的Key不存在时,返回的值</param>
        /// <returns>Key值</returns>
        public static float GetConfigData(string file, string szKeyName, float fDefaultValue)
        {
            string szValue = GetConfigData(file, szKeyName, fDefaultValue.ToString());
            try
            {
                return float.Parse(szValue);
            }
            catch
            {
                return fDefaultValue;
            }
        }

        /// <summary>
        ///  读取指定的配置文件中指定Key的值
        /// </summary>
        /// <param name="szKeyName">读取的Key名称</param>
        /// <param name="szDefaultValue">指定的Key不存在时,返回的值</param>
        /// <returns>Key值</returns>
        public static bool GetConfigData(string file, string szKeyName, bool bDefaultValue)
        {
            string szValue = GetConfigData(file, szKeyName, bDefaultValue.ToString());
            try
            {
                return bool.Parse(szValue);
            }
            catch
            {
                return bDefaultValue;
            }
        }

        /// <summary>
        /// 获取XPath指向的单一XML节点
        /// </summary>
        /// <param name="clsRootNode">XPath所在的根节点</param>
        /// <param name="szXPath">XPath表达式</param>
        /// <returns>XmlNode</returns>
        private static XmlNode SelectXmlNode(XmlNode clsRootNode, string szXPath)
        {
            if (clsRootNode == null || IsEmptyString(szXPath))
                return null;
            try
            {
                return clsRootNode.SelectSingleNode(szXPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取XPath指向的XML节点集
        /// </summary>
        /// <param name="clsRootNode">XPath所在的根节点</param>
        /// <param name="szXPath">XPath表达式</param>
        /// <returns>XmlNodeList</returns>
        private static XmlNodeList SelectXmlNodes(XmlNode clsRootNode, string szXPath)
        {
            if (clsRootNode == null || IsEmptyString(szXPath))
                return null;
            try
            {
                return clsRootNode.SelectNodes(szXPath);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  保存指定Key的值到指定的配置文件中
        /// </summary>
        /// <param name="szKeyName">要被修改值的Key名称</param>
        /// <param name="szValue">新修改的值</param>
        public static bool WriteConfigData(string file, string szKeyName, string szValue)
        {
            string szConfigFile = file;// string.Format("{0}\\{1}", GetWorkDirectory(), CONFIG_FILE);
            if (!File.Exists(szConfigFile))
            {
                if (!CreateXmlFile(szConfigFile, "main"))
                    return false;
            }
            XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);

            string szXPath = "/main/" + szKeyName;//string.Format("/SystemConfig/%s", szKeyName);
            XmlNode clsXmlNode = SelectXmlNode(clsXmlDoc, szXPath);
            if (clsXmlNode == null)
            {
                clsXmlNode = CreateXmlNode(clsXmlDoc, szKeyName);
            }
            clsXmlNode.InnerText = szValue;
            return SaveXmlDocument(clsXmlDoc, szConfigFile);
        }

        /// <summary>
        /// 创建一个XmlNode并添加到文档
        /// </summary>
        /// <param name="clsParentNode">父节点</param>
        /// <param name="szNodeName">结点名称</param>
        /// <returns>XmlNode</returns>
        private static XmlNode CreateXmlNode(XmlNode clsParentNode, string szNodeName)
        {
            try
            {
                XmlDocument clsXmlDoc = null;
                if (clsParentNode.GetType() != typeof(XmlDocument))
                    clsXmlDoc = clsParentNode.OwnerDocument;
                else
                    clsXmlDoc = clsParentNode as XmlDocument;
                XmlNode clsXmlNode = clsXmlDoc.CreateNode(XmlNodeType.Element, szNodeName, string.Empty);
                if (clsParentNode.GetType() == typeof(XmlDocument))
                {
                    clsXmlDoc.LastChild.AppendChild(clsXmlNode);
                }
                else
                {
                    clsParentNode.AppendChild(clsXmlNode);
                }
                return clsXmlNode;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <param name="buf"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static bool ReadBinHexSampleData(string file, string name, byte[] buf, int len)
        {
            int readByte = 0;

            string szConfigFile = file;//= string.Format("{0}\\{1}", GetWorkDirectory(), CONFIG_FILE);
            if (!File.Exists(szConfigFile))
            {
                return false;
            }

            XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);
            XmlTextReader xmlTextReader = new XmlTextReader(szConfigFile);
            XmlElement theImage = (XmlElement)clsXmlDoc.SelectSingleNode("/main/" + name);
            xmlTextReader.MoveToElement();
            while (xmlTextReader.Read())
            {
                if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == name)
                {
                    //readByte = xmlTextReader.ReadBase64(bufPlus, 0, len);
                    readByte = xmlTextReader.ReadBinHex(buf, 0, len);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 序列化存储数据（类）
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool WriteBase64Data(string file, string name, object obj)
        {
            string szConfigFile = file; //string.Format("{0}\\{1}", GetWorkDirectory(), CONFIG_FILE);
            if (!File.Exists(szConfigFile))
            {
                if (!CreateXmlFile(szConfigFile, "main"))
                    return false;
            }

            //序列化
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream memStream = new System.IO.MemoryStream();
            serializer.Serialize(memStream, obj);
            byte[] buf = memStream.GetBuffer();

            XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);
            string textString = System.Convert.ToBase64String(buf);
            XmlText text = clsXmlDoc.CreateTextNode(textString);

            XmlNode node = clsXmlDoc.SelectSingleNode("main/" + name);
            if (node != null)
            {
                node.InnerText = textString;
            }
            else
            {
                XmlElement elem = clsXmlDoc.CreateElement(name);
                clsXmlDoc.DocumentElement.AppendChild(elem);
                clsXmlDoc.DocumentElement.LastChild.AppendChild(text);
            }

            return SaveXmlDocument(clsXmlDoc, szConfigFile);
        }

        /// <summary>
        /// 读序列化的数据并反序列化
        /// </summary>
        /// <param name="file"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool ReadBase64Data(string file, string name, ref object obj)
        {
            int readByte = 0;

            string szConfigFile = file;//= string.Format("{0}\\{1}", GetWorkDirectory(), CONFIG_FILE);
            if (!File.Exists(szConfigFile))
            {
                return false;
            }
            try
            {
                XmlDocument clsXmlDoc = GetXmlDocument(szConfigFile);
                XmlTextReader xmlTextReader = new XmlTextReader(szConfigFile);
                XmlElement theImage = (XmlElement)clsXmlDoc.SelectSingleNode("/main/" + name);
                xmlTextReader.MoveToElement();
                while (xmlTextReader.Read())
                {
                    if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == name)
                    {
                        //读缓存
                        int len = theImage.InnerText.Length;
                        //byte[] buf = Convert.FromBase64String(theImage.InnerText);
                        byte[] buf = new byte[len];
                        readByte = xmlTextReader.ReadBase64(buf, 0, len);
                        //反序列化
                        System.IO.MemoryStream memStream = new MemoryStream(buf);
                        memStream.Position = 0;
                        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter deserializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        obj = deserializer.Deserialize(memStream);
                        memStream.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                obj = null;
                return false;
            }
            return false;

        }

    }
}
