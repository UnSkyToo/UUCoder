using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UUCoder.CodeElement
{
    public class UDataDocument : UDataElement
    {
        private UDataParser mParser;

        public UDataDocument()
        {
            this.mParser = new UDataParser();
        }

        public void Load(string filePath)
        {
            Items.Clear();

            mParser.Load(filePath);

            /*UDataElement Environment = ParseElement();
            UDataElement Editor = ParseElement();
            UDataElement Language = ParseElement();

            Items.Add(Environment);
            Items.Add(Editor);
            Items.Add(Language);*/

            while (true)
            {
                UDataElement element = ParseElement();

                if (element == null)
                {
                    break;
                }

                Items.Add(element);
            }
        }

        public void Save(string filePath)
        {
            try
            {
                StreamWriter fp = new StreamWriter(filePath, false, Encoding.Default);

                SaveElement(fp, this, 0);

                fp.Close();
                fp.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 配置表分析
        /// </summary>
        /// <returns></returns>
        private UDataElement ParseElement()
        {
            try
            {
                // 如果下一个数据为空，则分析完成
                if (mParser.PeekNextData() == string.Empty)
                {
                    return null;
                }

                // 获取根目录
                UDataElement element = new UDataElement();

                // 分析指定格式
                mParser.GetNextData("[");

                element.Name = mParser.GetNextData();
                element.Value = string.Empty;

                mParser.GetNextData("]");

                while (true)
                {
                    // 循环分析子项
                    string nextData = mParser.PeekNextData();

                    switch (nextData)
                    {
                        // 如果是“<”表示为子项数据
                        case "<":
                            {
                                // 按特定格式保存数据
                                UDataElement de = new UDataElement();

                                // 自定义数据
                                // 名称
                                mParser.GetNextData("<");
                                mParser.GetNextData("Name");
                                mParser.GetNextData("=");
                                de.Name = mParser.GetNextData();
                                mParser.GetNextData(" ");
                                // 说明
                                mParser.GetNextData("Tag");
                                mParser.GetNextData("=");
                                de.Tag = mParser.GetNextData();
                                mParser.GetNextData(" ");
                                // 内容
                                mParser.GetNextData("Value");
                                mParser.GetNextData("=");
                                de.Value = mParser.GetNextData();
                                mParser.GetNextData(">");

                                element.Items.Add(de);
                            }
                            break;
                        // 嵌套的配置块
                        case "[":
                            mParser.Save();

                            mParser.GetNextData("[");

                            // 配置块结束标志
                            if (mParser.PeekNextData() == "/")
                            {
                                mParser.GetNextData("/");
                                mParser.GetNextData(element.Name);
                                mParser.GetNextData("]");

                                return element;
                            }
                            else
                            {
                                // 循环分析
                                mParser.Load();

                                UDataElement newElement = ParseElement();

                                if (newElement == null)
                                {
                                    return element;
                                }
                                else
                                {
                                    element.Items.Add(newElement);
                                }
                            }

                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SaveElement(StreamWriter fp, UDataElement element, int tab)
        {
            try
            {
                foreach (UDataElement ue in element.Items)
                {
                    for (int i = 0; i < tab; i++)
                    {
                        fp.Write('\t');
                    }

                    if (ue.HasItem)
                    {
                        fp.WriteLine("[" + ue.Name + "]");

                        SaveElement(fp, ue, tab + 1);

                        for (int i = 0; i < tab; i++)
                        {
                            fp.Write('\t');
                        }

                        fp.WriteLine("[/" + ue.Name + "]");
                        fp.WriteLine();
                    }
                    else
                    {
                        if (ue.Name == "\"")
                        {
                            fp.WriteLine("<Name=\"\\\"\" Tag=\"" + ue.Tag + "\" Value=\"" + ue.Value + "\">");
                        }
                        else if (ue.Name == "\\")
                        {
                            fp.WriteLine("<Name=\"\\\\\" Tag=\"" + ue.Tag + "\" Value=\"" + ue.Value + "\">");
                        }
                        else
                        {
                            fp.WriteLine("<Name=\"" + ue.Name + "\" Tag=\"" + ue.Tag + "\" Value=\"" + ue.Value + "\">");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
