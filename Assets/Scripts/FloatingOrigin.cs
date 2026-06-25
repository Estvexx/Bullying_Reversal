using UnityEngine;

public class FloatingOrigin : MonoBehaviour {
    [SerializeField] private Player player;
    [SerializeField] private SpawnPlatform spawnPlatform;
    [SerializeField] private InimigoController inimigo;
    [SerializeField] private float limiteZ = 1000f;

    private void LateUpdate() {
        if (player == null || player.transform.position.z < limiteZ) return;

        float deslocamentoZ = -player.transform.position.z;

        player.AjustarOrigem(deslocamentoZ);

        if (spawnPlatform != null)
            spawnPlatform.AjustarOrigem(deslocamentoZ);

        if (inimigo != null)
            inimigo.AjustarOrigem(deslocamentoZ);
    }
}
