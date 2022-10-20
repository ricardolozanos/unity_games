using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    float lastFall = 0.0f;
    // Use this for initialization
    void Start()
    {
        if (!IsValidPiecePosition())
        {
            Debug.Log("GAMEOVER");
            Destroy(this.gameObject);
        }

    }


    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePieceHorizontally(-1);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePieceHorizontally(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            Rotation();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || (Time.time - lastFall)>1.0f ) 
        {
            MoveDown();

            lastFall = Time.time;
        }

        
    }

    void MoveDown()
    {

        this.transform.position += new Vector3(0, -1, 0);
        if (IsValidPiecePosition())
        {
            UpdateGrid();
        }
        else
        {
            this.transform.position += new Vector3(0, 1, 0);

            GridHelper.DeleteAllFullRows();//eliminar piezas?

            FindObjectOfType<PieceSpawner>().SpawnNextPiece();

            //deshabilitar script

            this.enabled = false;

            
        }
    }

    void Rotation()
    {
        this.transform.Rotate(0, 0, -90);
        if (IsValidPiecePosition())
        {
            UpdateGrid();
        }
        else
        {
            this.transform.Rotate(0, 0, 90);
        }
    }
    


    void MovePieceHorizontally (int direction)
    {
        
            //muevo pieza a izq
            this.transform.position += new Vector3(direction, 0, 0);

            //comprobar si la nueva pos es validad
            if (IsValidPiecePosition())
            {

                UpdateGrid();
            }
            else
            {
                //si no es valida revierto el movimiento
                this.transform.position += new Vector3(-direction, 0, 0);
            }
     
         

          

    }

     bool IsValidPiecePosition()
    {
        foreach (Transform block in this.transform)
        {
            //posicion de los hijos de la pieza
            Vector2 pos = GridHelper.RoundVector(block.position);

            if (!GridHelper.isInsideBorders(pos))
            {
                return false;
            }

            Transform possibleObject = GridHelper.grid[(int)pos.x, (int)pos.y];

            if (possibleObject!=null && possibleObject.parent!=this.transform)
            {
                return false;
            }
        }

        return true;
    }

    void UpdateGrid()
    {
        for (int y=0; y < GridHelper.height; y++)
        {
            for(int x = 0; x < GridHelper.width; x++)
            {

                if(GridHelper.grid[x,y] != null)
                {
                    if (GridHelper.grid[x,y].parent == this.transform)
                    {
                        GridHelper.grid[x,y] = null;
                    }
                }
            }
        }

        foreach(Transform block in this.transform)
        {
            Vector2 pos = GridHelper.RoundVector(block.position);

            GridHelper.grid[(int)pos.x, (int)pos.y] = block;
        }
    }




}
