using UnityEngine;
using System.Collections;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public GameObject Door = null;
	//public GameObject SubWall = null;

	//public GameObject[] Flowers;
	//public GameObject[] Plants;
	//public GameObject Flowers = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;
	//public GameObject GoalPrefab = null;

	int randomRow;
	int randomColumn;
	//int randomFlower;
	//int randomPlant;
	//float randomPosX;
	//float randomPosZ;

	private BasicMazeGenerator mMazeGenerator = null;

	void Start () {
		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}

		
		mMazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;

				//if (column / 2 == 0)
				//{
				//randomFlower = Random.Range(0, Flowers.Length);
				//randomPlant = Random.Range(0, Plants.Length);
				//randomPosX = Random.Range(column*CellWidth, (column*CellWidth)+CellWidth);
				//randomPosZ = Random.Range(row*CellHeight, (row*CellHeight)*CellHeight);

				//tmp = Instantiate(Flowers[randomFlower], new Vector3(randomPosX, 0, randomPosZ), Quaternion.Euler(0, 0, 0)) as GameObject;
				//tmp.transform.parent = transform;
				//
				//randomFlower = Random.Range(0, Flowers.Length);
				//
				//tmp = Instantiate(Flowers[randomFlower], new Vector3(randomPosZ, 0, randomPosX), Quaternion.Euler(0, 0, 0)) as GameObject;
				//tmp.transform.parent = transform;

				if (cell.WallRight){
					tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
					tmp.transform.parent = transform;
					//tmp = Instantiate(SubWall, new Vector3(x + CellWidth / 2, 0, z) + SubWall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
					//tmp.transform.parent = transform;
					//for(int i = 0; i < CellWidth; i ++)
					//{
					//	tmp = Instantiate(Plants[randomPlant], new Vector3(x + CellWidth / 2, 0, z) + Plants[randomPlant].transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
					//	tmp.transform.parent = transform;
					//}
				}
				if(cell.WallFront){
					tmp = Instantiate(Wall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
					tmp.transform.parent = transform;
					//tmp = Instantiate(SubWall, new Vector3(x, 0, z + CellHeight / 2) + SubWall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
					//tmp.transform.parent = transform;
				}
				if(cell.WallLeft){
					tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
					tmp.transform.parent = transform;
					//tmp = Instantiate(SubWall, new Vector3(x - CellWidth / 2, 0, z) + SubWall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
					//tmp.transform.parent = transform;
				}
				if(cell.WallBack){
					tmp = Instantiate(Wall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
					tmp.transform.parent = transform;
					//tmp = Instantiate(SubWall, new Vector3(x, 0, z - CellHeight / 2) + SubWall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
					//tmp.transform.parent = transform;
				}	//
				//if(cell.IsGoal && GoalPrefab != null){
				//	tmp = Instantiate(GoalPrefab,new Vector3(x,1,z), Quaternion.Euler(0,0,0)) as GameObject;
				//	tmp.transform.parent = transform;
				//}
			}
		}
		if(Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}

		//Celda aleatoria para puerta:
		randomRow = Random.Range(0, Rows);
		randomColumn = Random.Range(0, Columns);
		
		float px = randomColumn * (CellWidth + (AddGaps ? .2f : 0));
		float pz = randomRow * (CellHeight + (AddGaps ? .2f : 0));
		MazeCell pcell = mMazeGenerator.GetMazeCell(randomRow, randomColumn);
		GameObject ptmp;
		if (pcell.WallRight)
		{
			ptmp = Instantiate(Door, new Vector3(px + CellWidth / 2, 0, pz) + Door.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
			ptmp.transform.parent = transform;
		}
		else if (pcell.WallFront)
		{
			ptmp = Instantiate(Door, new Vector3(px, 0, pz + CellHeight / 2) + Door.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
			ptmp.transform.parent = transform;
		}
		else if (pcell.WallLeft)
		{
			ptmp = Instantiate(Door, new Vector3(px - CellWidth / 2, 0, pz) + Door.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
			ptmp.transform.parent = transform;
		}
		else if (pcell.WallBack)
		{
			ptmp = Instantiate(Door, new Vector3(px, 0, pz - CellHeight / 2) + Door.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
			ptmp.transform.parent = transform;
		}   
	}	
}
