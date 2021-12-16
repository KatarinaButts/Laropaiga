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

    void Start()
    {
        //Empty
    }

    void Update()
    {
        //Empty
    }

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
        //Debug.Log("Entered InitializeDictionaryFromFile() function");
        //string text = " "; // assigned to allow first line to be read below
        //the entry to add to the list

        dictionaryDataXml = new XmlDocument();
        dictionaryDataXml.LoadXml(xmlTextAsset.text);
        XmlNodeList dialogueSections = dictionaryDataXml.SelectNodes("/Dictionary/DictionaryEntry");
        foreach (XmlNode dictionarySection in dialogueSections)
        {
            DictionaryEntry dictionaryEntry = new DictionaryEntry();
            dictionaryEntry.SetData(dictionarySection);
            dictionary.Add(dictionaryEntry);
        }

        /*
        //the reader to pull info from a file
        StreamReader reader = null;

        reader = sourceFile.OpenText();
        while(text != null)
        {
            text = reader.ReadLine();
            if(text!= null)
            {
                wordJapanese = text.ToString().Trim();
                //Debug.Log(wordJapanese);
                text = reader.ReadLine();

                wordEnglish = text.ToString().Trim();
                //Debug.Log(wordEnglish);
                text = reader.ReadLine();


                pronunciation = text.ToString().Trim();
                //Debug.Log(pronunciation);
                text = reader.ReadLine();


                partOfSpeech = text.ToString().Trim();
                //Debug.Log(partOfSpeech);
                text = reader.ReadLine();


                definition = text.ToString().Trim();
                //Debug.Log(definition);
                text = reader.ReadLine();


                image = text.ToString().Trim();
                //Debug.Log(image);
                //text = reader.ReadLine(); //don't over-read!

                entry = gameObject.AddComponent(typeof(DictionaryEntry)) as DictionaryEntry;

                //setting data
                entry.SetData(wordJapanese, wordEnglish, pronunciation, partOfSpeech, definition, image, searchWord);
                dictionary.Add(entry);
            }
        }
        reader.Close();
        text = " ";

        */
        /*
        //keep these outside the loop in order to check everything
        foreach (DictionaryEntry a in dictionary)
        {
            Debug.Log("Japanese Word: " + a.GetJapaneseWord() + ", English Word: " + a.GetEnglishWord());
        }
        */
    }

}
