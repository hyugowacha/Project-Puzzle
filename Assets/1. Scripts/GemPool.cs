using UnityEngine;
using UnityEngine.Pool;

public class GemPool : MonoBehaviour
{
    [SerializeField] private Gem[] gemPrefabs;

    [Header("오브젝트풀 세팅")]
    [SerializeField] private bool collectionCheck = true; //중복 반환 체크
    [SerializeField] private int defaultSize = 15; //최초 내부 용량
    [SerializeField] private int maxSize = 30; //오브젝트 최대 갯수

    private ObjectPool<Gem>[] pool;
    private Gem[] typePrefab;

    private int currentType;

    private void Awake()
    {
        int typeCount = gemPrefabs.Length;

        pool = new ObjectPool<Gem>[typeCount];
        typePrefab = new Gem[typeCount];


        for (int i = 0; i < gemPrefabs.Length; i++) 
        {
            int t = (int)gemPrefabs[i].type;
            typePrefab[t] = gemPrefabs[i];
        }

        for (int i = 0; i < typeCount; i++) 
        {
            pool[i] = new ObjectPool<Gem>
            (
                CreateGem, 
                OnGetGem, 
                OnReleaseGem, 
                OnDestroyGem, 
                collectionCheck, 
                defaultSize, 
                maxSize
            );

            for (int j = 0; j < defaultSize; j++) 
            {
                currentType = i;
                Gem g = pool[i].Get();
                pool[i].Release(g) ;
            }
        }
    }

    private Gem CreateGem() //오브젝트 생성
    {
        Gem prefab = typePrefab[currentType];
        Gem gem = Instantiate(prefab, transform);
        gem.gameObject.SetActive(false);
        return gem;
    }

    private void OnGetGem(Gem gem) //풀에서 오브젝트 가져옴
    {
        gem.gameObject.SetActive(true); 
    }

    private void OnReleaseGem(Gem gem) //풀로 오브젝트 반환 
    {
        gem.gameObject.SetActive(false);
        gem.transform.SetParent(transform);
    }

    private void OnDestroyGem(Gem gem) //오브젝트 파괴
    {
        Destroy(gem.gameObject);
    }

    public Gem Get(GemType type) 
    {
        int t = (int)type;
        currentType = t;
        return pool[t].Get();
    }

    public void Release(Gem gem)
    {
        pool[(int)gem.type].Release(gem);
    }
}
