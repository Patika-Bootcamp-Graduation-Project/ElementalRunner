using System.Collections.Generic;
using UnityEngine;

namespace Olcay.LevelGenerator
{
    public class LevelGenerator : MonoBehaviour
    {
        public List<Pattern> patterns;
        private GameObject pattern;

        [SerializeField] private GameObject patternBase;
        
        [Header("Index Settings")]
        [SerializeField] private int startPatternIndex;
        [SerializeField] private int emptyPatternIndex;
        [SerializeField] private int gatePatternIndex;
        [SerializeField] private int miniGamePatternIndex;
        [SerializeField] private int collectablePatternCount;
        private Vector3 nextPatternStartPoint;
        private int gatePatternCount => patterns[tag.CompareTo("Gate")].prefab.Count;

        private int emptyPatternCount => patterns[tag.CompareTo("Empty")].prefab.Count;
        [Header("Gate Pattern Settings")]
        [SerializeField] private int gateStartPoint;
        [SerializeField] private int gateCount;
        [SerializeField] private int gateSpawnFrequency;
        [Header("Empty Pattern Settings")]
        [SerializeField] private int emptyPatternStartPoint;
        [SerializeField] private int emptyPatternSpawnFrequency;
        
        private int gateCountFlag = 0;
        private int emptyCountFlag = 0;
        private int randomPatternIndex;

        private void Awake()
        {
            GeneratePattern();
        }

        private void GeneratePattern()
        {
            pattern = Instantiate(patterns[startPatternIndex]
                .prefab[0], nextPatternStartPoint, Quaternion.identity);
            pattern.transform.parent = patternBase.transform;
            nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;

            for (int i = 0; i < collectablePatternCount; i++)
            {
                emptyCountFlag++;
                if (gateStartPoint == i && gateCountFlag <= gateCount)
                {
                    pattern = Instantiate(patterns[gatePatternIndex]
                            .prefab[Random.Range(0, gatePatternCount)],
                        nextPatternStartPoint, Quaternion.identity);
                    pattern.transform.parent = patternBase.transform;
                    nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;

                    gateStartPoint = Random.Range(gateStartPoint + gateSpawnFrequency, collectablePatternCount - 3);

                    gateCountFlag++;
                }
                
                if (emptyPatternStartPoint==i)
                {
                    pattern = Instantiate(patterns[emptyPatternIndex]
                            .prefab[Random.Range(0, emptyPatternCount)],
                        nextPatternStartPoint, Quaternion.identity);
                    pattern.transform.parent = patternBase.transform;
                    nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;
                    if (emptyCountFlag>emptyPatternSpawnFrequency)
                    {
                        emptyPatternStartPoint += emptyCountFlag;
                        emptyCountFlag = 0;
                    }
                }

                int randomPatternIndex =
                    Random.Range(0,
                        patterns.Count -
                        4); //-4 its because of for this game we dont have obstacle logic like usual xd. We are just spawning collectable patterns which is they are have also obstacles
                pattern = Instantiate(
                    patterns[randomPatternIndex].prefab[Random.Range(0, patterns[randomPatternIndex].prefab.Count)],
                    nextPatternStartPoint, Quaternion.identity);
                pattern.transform.parent = patternBase.transform;
                nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;
            }

            pattern = Instantiate(patterns[miniGamePatternIndex]
                .prefab[0], nextPatternStartPoint, Quaternion.identity);
            pattern.transform.parent = patternBase.transform;
            nextPatternStartPoint = pattern.transform.GetChild(0).transform.position;
        }
    }


    [System.Serializable]
    public class Pattern
    {
        public string tag;
        public List<GameObject> prefab;
    }
}