/*
    Copyright (C) 2018 Fernando Porrino Serrano.
    This software it's under the terms of the GNU Affero General Public License version 3.
    Please, refer to (https://github.com/FherStk/DocumentPlagiarismChecker/blob/master/LICENSE) for further licensing details.
 */
 
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using DocumentPlagiarismChecker.Core;

namespace DocumentPlagiarismChecker.Comparators.DocumentWordCounter
{
    /// <summary>
    /// El contador de palabras compara archivos y cuenta cuantas palabras aparecen en cada archivo, luego calcula cuantas veces se repiten en esos documentos.un alto porcentaje de coincidencias podria significar que es una copia

    /// </summary>
    /// <typeparam name="Document"></typeparam>
    internal class Comparator: Core.BaseComparator<Document>
    {  
        /// <summary>
        /// Crea una nueva instancia para el Comparador.
        /// </summary>
        /// <param name="fileLeftPath">The left side file's path.</param>
        /// <param name="fileRightPath">The right side file's path.</param>
        /// <returns></returns>
        public Comparator(string fileLeftPath, string fileRightPath, string sampleFilePath=null): base(fileLeftPath, fileRightPath, sampleFilePath){
        }  
        
        /// <summary>
        /// Cuenta cuántas palabras y cuántas veces aparecen dentro de cada documento, para verifica el porcentaje correspondiente.        /// </summary>
        /// <returns>The matching's results.</returns>
        public override ComparatorMatchingScore Run(){
<<<<<<< HEAD
        // Contando repeticiones de cada palabras en cada documento (izquierda y derecha).            Dictionary<string, int[]> counter = new Dictionary<string, int[]>();
=======
            //Cuenta la frecuencia de palabras por cada documento (iquierda y derecha).
            Dictionary<string, int[]> counter = new Dictionary<string, int[]>();
>>>>>>> f0cb799c19f7b6d7e33756441e7638e1f19c7c7c
            foreach(string word in this.Left.WordAppearances.Select(x => x.Key)){
                if(!counter.ContainsKey(word)) counter.Add(word, new int[]{0, 0});
                counter[word][0] += Left.WordAppearances[word];
            }

            foreach(string word in this.Right.WordAppearances.Select(x => x.Key)){
                if(!counter.ContainsKey(word)) counter.Add(word, new int[]{0, 0});
                counter[word][1] += Right.WordAppearances[word];
            }

<<<<<<< HEAD
            //contando las palabras del archivo original.
=======
// Contando las apariciones de la palabra del archivo de muestra, para ignorar las de los archivos anteriores.
>>>>>>> f0cb799c19f7b6d7e33756441e7638e1f19c7c7c
            if(this.Sample != null){
                 foreach(string word in this.Sample.WordAppearances.Select(x => x.Key)){
                    if(counter.ContainsKey(word)){
                        counter[word][0] = Math.Max(0, counter[word][0] - Sample.WordAppearances[word]);
                        counter[word][1] = Math.Max(0, counter[word][1] - Sample.WordAppearances[word]);
                        
                        if(counter[word][0] == 0 && counter[word][1] == 0)
                            counter.Remove(word);
                    }                    
                }
            }

<<<<<<< HEAD
            //Definiendo los encabezados de resultado.
            ComparatorMatchingScore cr = new ComparatorMatchingScore("Document Word Counter", DisplayLevel.FULL);            
=======

// Definiendo los encabezados de resultados    
        ComparatorMatchingScore cr = new ComparatorMatchingScore("Document Word Counter", DisplayLevel.FULL);            
>>>>>>> f0cb799c19f7b6d7e33756441e7638e1f19c7c7c
            cr.DetailsCaption = new string[] { "Word", "Count left", "Count right", "Matching" };
            cr.DetailsFormat = new string[]{"{0}", "{0}", "{0}", "{0:P2}"};

            //Calcula las coincidencias de cada palabra uno a uno de manera individual.
            float match = 0;
            int left, right = 0;
            foreach(string word in counter.Select(x => x.Key)){                
                left = counter[word][0];
                right = counter[word][1];                

                if(left == 0 || right == 0) match = 0;
                else match = (left < right ? (float)left / (float)right : (float)right / (float)left);

                cr.AddMatch(match);
                cr.DetailsData.Add(new object[]{word, left, right, match});                
            }                                    
            
            return cr;
        }        
    }   
}