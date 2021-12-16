using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class DictionaryEntry/* : MonoBehaviour*/
{
    string wordJapanese;
    string wordEnglish;
    string pronunciation;
    string partOfSpeech;
    string definition;
    string image;
    string searchWord;
    bool learned;

    //ToDo:
    //string verbConjugationType;  
    //string example;               
    //string kanjiImage;
    //audio
    /*
    void Awake()
    {
        learned = false;
    }

    void Start()
    {
        //Empty
    }

    void Update()
    {
        //Empty
    }
    */
    //Setters
    //Initial setup of entry
    public void SetData(XmlNode dictSection)
    {
        wordJapanese = dictSection["wordJapanese"].InnerText;
        wordEnglish = dictSection["wordEnglish"].InnerText;
        pronunciation = dictSection["pronunciation"].InnerText;
        partOfSpeech = dictSection["partOfSpeech"].InnerText;
        definition = dictSection["definition"].InnerText;
        image = dictSection["image"].InnerText;
        searchWord = dictSection["searchWord"].InnerText;
    }
    public void LearnWord()
    {
        learned = true;
    }

    //Getters
    public bool GetLearned()
    {
        return learned;
    }

    public string GetJapaneseWord()
    {
        return wordJapanese;
    }
    public string GetEnglishWord()
    {
        return wordEnglish;
    }
    public string GetPronunciation()
    {
        return pronunciation;
    }
    public string GetDefinition()
    {
        return definition;
    }
    public string GetPartOfSpeech()
    {
        return partOfSpeech;
    }
    public string GetImage()
    {
        return image;
    }

    public string GetSearchWord()
    {
        return searchWord;
    }

}
