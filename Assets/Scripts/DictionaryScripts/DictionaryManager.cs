using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;


public class DictionaryManager : MonoBehaviour
{
    List<DictionaryEntry> dictionary = new List<DictionaryEntry>();
    XmlDocument dictionaryDataXml;
    DictionaryEntry activeWord;

    //Getters
    public List<DictionaryEntry> GetDictionaryEntries()
    {
        return dictionary;
    }

    public DictionaryEntry GetActiveWord()
    {
        return activeWord;
    }

    //Setters
    public void SetActiveWord(DictionaryEntry word)
    {
        activeWord = word;
    }

    public void InitializeDictionaryFromFile()
    {
        TextAsset xmlTextAsset = Resources.Load<TextAsset>("TextFiles/DictionaryEntries");

        dictionaryDataXml = new XmlDocument();
        dictionaryDataXml.LoadXml(xmlTextAsset.text);
        XmlNodeList dialogueSections = dictionaryDataXml.SelectNodes("/Dictionary/DictionaryEntry");
        foreach (XmlNode dictionarySection in dialogueSections)
        {
            DictionaryEntry dictionaryEntry = new DictionaryEntry();
            dictionaryEntry.SetData(dictionarySection);
            dictionary.Add(dictionaryEntry);
        }
    }

}
