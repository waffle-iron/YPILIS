﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_TemplateName;
        protected string m_Text;
        protected string m_TranscribedText;
        protected string m_TranscribedWords;

        protected List<TemplateWord> m_WordList;
        protected YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;

        public DictationTemplate()
        {
            this.m_WordList = new List<TemplateWord>();
            this.m_SpecimenCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenCollection();
        }

        public List<TemplateWord> WordList
        {
            get { return this.m_WordList; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return this.m_SpecimenCollection; }
        }

        public string TemplateName
        {
            get { return this.m_TemplateName; }
            set
            {
                if (this.m_TemplateName != value)
                {
                    this.m_TemplateName = value;
                    this.NotifyPropertyChanged("TemplateName");
                }
            }
        }

        public string Text
        {
            get { return this.m_Text; }
            set 
            {
                if (this.m_Text != value)
                {
                    this.m_Text = value;
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        public string TranscribedText
        {
            get { return this.m_TranscribedText; }
            set 
            {
                if (this.m_TranscribedText != value)
                {
                    this.m_TranscribedText = value;
                    this.NotifyPropertyChanged("TranscribedText");
                }
            }
        }

        public string TranscribedWords
        {
            get { return this.m_TranscribedWords; }
            set
            {
                if (this.m_TranscribedWords != value)
                {
                    this.m_TranscribedWords = value;
                    this.NotifyPropertyChanged("TranscribedWords");
                }
            }
        }   

        public void BuildText()
        {
            if (string.IsNullOrEmpty(this.m_TranscribedWords) == false)
            {
                this.m_TranscribedText = this.m_Text;              
                string [] delimeter = { "\r\n" };
                string[] lineSplit = this.m_TranscribedWords.Split(delimeter, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < lineSplit.Length; i++)
                {
                    string text = lineSplit[i];
                    if (i < this.m_WordList.Count)
                    {
                        string placeHolder = this.m_WordList[i].PlaceHolder;
                        this.m_TranscribedText = this.m_TranscribedText.Replace(placeHolder, text);
                    }
                }
                this.NotifyPropertyChanged("TranscribedText");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }     
    }
}