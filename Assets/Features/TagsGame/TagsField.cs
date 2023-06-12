using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.TagsGame
{
    //[Serializable]
    public class TagsField : MonoBehaviour
    {
        [SerializeField] private List<TagsPoint> points;
        [SerializeField] private Knuckle[] knuckles;
        [SerializeField] private LeverGates leverGates;
        [SerializeField] private float waitTimeOpenGates;
        
        private bool _canPlay;
        private const ushort c_horizontalMoveLimit = 3;
        private const ushort c_verticalMoveLimit = 1;

        private void Start()
        {
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Knuckle != null)
                {
                    points[i].Knuckle.lifted = true;
                    points[i].Knuckle.Particle.SetActive(false);
                    points[i].Knuckle.InitInteract(TryMove);
                }
            }
            
            for (int i = 0; i < knuckles.Length; i++)
            {
                knuckles[i].InitLift(AddKnuckleToPoint);
            }
            _canPlay = CanStartGame();
        }

        public void AddKnuckleToPoint(GameObject knuckleObject)
        {
            if (knuckleObject.TryGetComponent<Knuckle>(out var knuckle))
            {
                int pointIndex = GetRandomFreePointIndex();
                points[pointIndex].Knuckle = knuckle;
                points[pointIndex].Knuckle.InitInteract(TryMove);
                MoveToPoint(points[pointIndex]);
                _canPlay = CanStartGame();
            }
        }

        private int GetRandomFreePointIndex()
        {
            var freePoints = new List<int>();

            for (int i = 0; i < points.Count; i++)
                if (points[i].Knuckle == null)
                    freePoints.Add(i);
            int randomIndex = Random.Range(0, freePoints.Count-1);
            
            return freePoints[randomIndex];
        }

        private void MoveToPoint(TagsPoint point)
        {
            point.Knuckle.transform.position = point.transform.position;
        }
        
        public int GetEmptyPoint()
        {
            for (int i = 0; i < points.Count; i++)
                if (points[i].Knuckle == null)
                    return i;
            return -1;
        }

        public void TryMove(Knuckle knuckle)
        {
            if (_canPlay)
            {
                int emptyPoint = GetEmptyPoint();
                var possibleKnuckles = GetPossibleMoves(emptyPoint);
                
                for (int i = 0; i < possibleKnuckles.Count; i++)
                {
                    if (possibleKnuckles[i].Id == knuckle.Id)
                    {
                        foreach (var point in points)
                        {
                            if (point.Knuckle?.Id == knuckle.Id)
                            {
                                point.Knuckle = null;
                                break;
                            }
                        }

                        StartCoroutine(knuckle.Move(points[emptyPoint]));
                    }
                }
            }

            if (CheckGameEnding())
            {
                _canPlay = false;
                StartCoroutine(OpenGates());
            }
        }

        private IEnumerator OpenGates()
        {
            yield return new WaitForSeconds(waitTimeOpenGates);
            leverGates.Open();
        }
        public List<Knuckle> GetPossibleMoves(int emptyPoint)
        {
            List<Knuckle> knuckles = new List<Knuckle>();
            if (emptyPoint + c_horizontalMoveLimit < points.Count)
                knuckles.Add(points[emptyPoint + c_horizontalMoveLimit].Knuckle);
            if (emptyPoint - c_horizontalMoveLimit > 0)
                knuckles.Add(points[emptyPoint - c_horizontalMoveLimit].Knuckle);
            if (emptyPoint + c_verticalMoveLimit < points.Count)
                knuckles.Add(points[emptyPoint + c_verticalMoveLimit].Knuckle);
            if (emptyPoint - c_verticalMoveLimit > 0)
                knuckles.Add(points[emptyPoint - c_verticalMoveLimit].Knuckle);
            return knuckles;
        }

        public bool CheckGameEnding()
        {
            if (points[^1].Knuckle != null)
                return false;
            
            for (int i = 0; i < points.Count - 1; i++)
                if (points[i].Knuckle.Id != i)
                    return false;

            return true;
        }

        private bool CanStartGame()
        {
            int isEmpty = 0;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Knuckle == null)
                    isEmpty++;
            }

            return isEmpty == 1;
        }
    }
}