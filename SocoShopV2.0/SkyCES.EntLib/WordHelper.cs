using Microsoft.Office.Interop.Word;
using System;
using System.Reflection;

namespace SkyCES.EntLib
{
    public class WordHelper
    {
        private ApplicationClass applicationClass = new ApplicationClass();
        private Document document;
        private object missing = Missing.Value;

        public void ClearStyle()
        {
            this.applicationClass.Selection.Font.Bold = 0;
            this.applicationClass.Selection.Font.Italic = 0;
            this.applicationClass.Selection.Font.Subscript = 0;
        }

        public void GotoBookMark(string strBookMarkName)
        {
            object what = -1;
            object name = strBookMarkName;
            this.applicationClass.Selection.GoTo(ref what, ref this.missing, ref this.missing, ref name);
        }

        public void GoToDownCell()
        {
            object wdLine = WdUnits.wdLine;
            this.applicationClass.Selection.MoveDown(ref wdLine, ref this.missing, ref this.missing);
        }

        public void GoToLeftCell()
        {
            object wdCell = WdUnits.wdCell;
            this.applicationClass.Selection.MoveLeft(ref wdCell, ref this.missing, ref this.missing);
        }

        public void GoToRightCell()
        {
            object wdCell = WdUnits.wdCell;
            this.applicationClass.Selection.MoveRight(ref wdCell, ref this.missing, ref this.missing);
        }

        public void GoToTheBeginning()
        {
            object wdStory = WdUnits.wdStory;
            this.applicationClass.Selection.HomeKey(ref wdStory, ref this.missing);
        }

        public void GoToTheEnd()
        {
            object wdStory = WdUnits.wdStory;
            this.applicationClass.Selection.EndKey(ref wdStory, ref this.missing);
        }

        public void GoToTheTable(int ntable)
        {
            object wdTable = WdUnits.wdTable;
            object wdGoToFirst = WdGoToDirection.wdGoToFirst;
            object count = 1;
            this.applicationClass.Selection.GoTo(ref wdTable, ref wdGoToFirst, ref count, ref this.missing);
            this.applicationClass.Selection.Find.ClearFormatting();
            this.applicationClass.Selection.Text = "";
        }

        public void GoToUpCell()
        {
            object wdLine = WdUnits.wdLine;
            this.applicationClass.Selection.MoveUp(ref wdLine, ref this.missing, ref this.missing);
        }

        public void InsertLineBreak()
        {
            this.applicationClass.Selection.TypeParagraph();
        }

        public void InsertLineBreak(int nline)
        {
            for (int i = 0; i < nline; i++)
            {
                this.applicationClass.Selection.TypeParagraph();
            }
        }

        public void InsertPagebreak()
        {
            object type = 7;
            this.applicationClass.Selection.InsertBreak(ref type);
        }

        public void InsertText(string strText)
        {
            this.applicationClass.Selection.TypeText(strText);
        }

        public void New()
        {
            object template = Missing.Value;
            this.document = this.applicationClass.Documents.Add(ref template, ref template, ref template, ref template);
            this.document.Activate();
        }

        public void Open(string strFileName)
        {
            object fileName = strFileName;
            object readOnly = false;
            object visible = true;
            this.document = this.applicationClass.Documents.Open(ref fileName, ref this.missing, ref readOnly, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref visible, ref this.missing, ref this.missing, ref this.missing, ref this.missing);
            this.document.Activate();
        }

        public void Quit()
        {
            object saveChanges = Missing.Value;
            this.applicationClass.Application.Quit(ref saveChanges, ref saveChanges, ref saveChanges);
        }

        public bool ReplaceText(string findStr, string replaceStr)
        {
            object wdReplaceAll = WdReplace.wdReplaceAll;
            this.applicationClass.Selection.Find.ClearFormatting();
            object findText = findStr;
            this.applicationClass.Selection.Find.Replacement.ClearFormatting();
            this.applicationClass.Selection.Find.Replacement.Text = replaceStr;
            return this.applicationClass.Selection.Find.Execute(ref findText, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref wdReplaceAll, ref this.missing, ref this.missing, ref this.missing, ref this.missing);
        }

        public void Save()
        {
            this.document.Save();
        }

        public void SaveAs(string strFileName)
        {
            object fileName = strFileName;
            this.document.SaveAs(ref fileName, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing);
        }

        public void SaveAsHtml(string strFileName)
        {
            object fileName = strFileName;
            object fileFormat = 8;
            this.document.SaveAs(ref fileName, ref fileFormat, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing, ref this.missing);
        }

        public void SetAlignment(string strType)
        {
            string str = strType;
            if (str != null)
            {
                if (!(str == "Center"))
                {
                    if (str == "Left")
                        this.applicationClass.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    else if (str == "Right")
                        this.applicationClass.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    else if (str == "Justify") this.applicationClass.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphJustify;
                }
                else
                    this.applicationClass.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            }
        }

        public void SetFont(string strType)
        {
            string str = strType;
            if (str != null)
            {
                if (!(str == "Bold"))
                {
                    if (str == "Italic")
                        this.applicationClass.Selection.Font.Italic = 1;
                    else if (str == "Underlined") this.applicationClass.Selection.Font.Subscript = 0;
                }
                else
                    this.applicationClass.Selection.Font.Bold = 1;
            }
        }

        public void SetFontColor(WdColor color)
        {
            this.applicationClass.Selection.Font.Color = color;
        }

        public void SetFontName(string strType)
        {
            this.applicationClass.Selection.Font.Name = strType;
        }

        public void SetFontSize(int nSize)
        {
            this.applicationClass.Selection.Font.Size = nSize;
        }
    }
}

