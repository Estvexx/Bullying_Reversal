using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour {
    private enum TipoObstaculo {
        Nenhum,
        Bloqueia,
        Saltar,
        Rolar
    }

    private struct EstadoPossivel {
        public int lane;
        public float ultimaAcaoZ;

        public EstadoPossivel(int lane, float ultimaAcaoZ) {
            this.lane = lane;
            this.ultimaAcaoZ = ultimaAcaoZ;
        }
    }

    [SerializeField] private Player player;
    private Transform playerTransform;

    [SerializeField] private GameObject obstaculoBloqueiaPrefab;
    [SerializeField] private GameObject obstaculoSaltarPrefab;
    [SerializeField] private GameObject obstaculoRolarPrefab;

    [SerializeField] private float laneWidth = 2.5f;
    [SerializeField] private float distanciaSpawn = 70f;
    [SerializeField] private float distanciaInicial = 45f;
    [SerializeField] private float spawnY = 0f;
    [SerializeField] private int tentativasPorLinha = 25;

    [SerializeField] private float tempoReacao = 0.75f;
    [SerializeField] private float margemEntreLinhas = 5f;
    [SerializeField] private float tempoMinimoEntreAcoes = 0.85f;
    [SerializeField] private float margemMudancaLane = 3f;

    [SerializeField] private float distanciaParaDestruir = 30f;

    private readonly List<EstadoPossivel> estadosPossiveis = new();
    private readonly List<EstadoPossivel> estadosTemporarios = new();
    private readonly List<GameObject> obstaculosCriados = new();

    private float proximoSpawnZ;
    private float ultimoSpawnZ;

    private void Start() {
        playerTransform = player.transform;
        if (playerTransform == null)
            playerTransform = player.transform;

        proximoSpawnZ = playerTransform.position.z + distanciaInicial;
        ultimoSpawnZ = playerTransform.position.z;

        estadosPossiveis.Add(new EstadoPossivel(0, -999f));
    }

    private void Update() {
        if (player == null || !player.estaVivo) return;

        while (playerTransform.position.z + distanciaSpawn >= proximoSpawnZ) {
            TipoObstaculo[] linha = GerarLinhaValida(proximoSpawnZ);
            CriarLinha(linha, proximoSpawnZ);

            ultimoSpawnZ = proximoSpawnZ;
            proximoSpawnZ += DistanciaEntreLinhas();
        }

        LimparObstaculosAntigos();
    }

    private TipoObstaculo[] GerarLinhaValida(float z) {
        for (int i = 0; i < tentativasPorLinha; i++) {
            TipoObstaculo[] linha = GerarLinhaAleatoria();

            if (LinhaValida(linha, z)) {
                CopiarEstadosValidados();
                return linha;
            }
        }

        TipoObstaculo[] linhaSegura = {
            TipoObstaculo.Nenhum,
            TipoObstaculo.Nenhum,
            TipoObstaculo.Nenhum
        };

        LinhaValida(linhaSegura, z);
        CopiarEstadosValidados();

        return linhaSegura;
    }

    private TipoObstaculo[] GerarLinhaAleatoria() {
        TipoObstaculo[] linha = {
            TipoObstaculo.Nenhum,
            TipoObstaculo.Nenhum,
            TipoObstaculo.Nenhum
        };

        int quantidade = Random.Range(1, MaxObstaculosPorLinha() + 1);

        for (int i = 0; i < quantidade; i++) {
            int laneIndex = Random.Range(0, 3);

            if (linha[laneIndex] != TipoObstaculo.Nenhum) {
                i--;
                continue;
            }

            linha[laneIndex] = TipoAleatorio();
        }

        return linha;
    }

    private TipoObstaculo TipoAleatorio() {
        int r = Random.Range(0, 100);

        if (r < 45) return TipoObstaculo.Bloqueia;
        if (r < 75) return TipoObstaculo.Saltar;

        return TipoObstaculo.Rolar;
    }

    private bool LinhaValida(TipoObstaculo[] linha, float z) {
        estadosTemporarios.Clear();

        float distancia = z - ultimoSpawnZ;

        foreach (EstadoPossivel estado in estadosPossiveis) {
            for (int lane = -1; lane <= 1; lane++) {
                if (!ConsegueChegarNaLane(estado.lane, lane, distancia))
                    continue;

                TipoObstaculo obstaculo = linha[LaneParaIndex(lane)];

                if (!ConseguePassarObstaculo(obstaculo, estado, z, out float novaUltimaAcaoZ))
                    continue;

                AdicionarEstadoTemporario(lane, novaUltimaAcaoZ);
            }
        }

        return estadosTemporarios.Count > 0;
    }

    private bool ConsegueChegarNaLane(int laneAtual, int laneDestino, float distancia) {
        int diferenca = Mathf.Abs(laneDestino - laneAtual);

        if (diferenca == 0)
            return true;

        float velocidade = Mathf.Max(player.velocidadeAtual, player.velocidadeBase);
        float tempoParaMudar = (laneWidth * diferenca) / player.laneSpeed;
        float distanciaNecessaria = velocidade * tempoParaMudar + margemMudancaLane;

        return distancia >= distanciaNecessaria;
    }

    private bool ConseguePassarObstaculo(TipoObstaculo tipo, EstadoPossivel estado, float z, out float novaUltimaAcaoZ) {
        novaUltimaAcaoZ = estado.ultimaAcaoZ;

        if (tipo == TipoObstaculo.Nenhum)
            return true;

        if (tipo == TipoObstaculo.Bloqueia)
            return false;

        if (z - estado.ultimaAcaoZ < DistanciaMinimaEntreAcoes())
            return false;

        novaUltimaAcaoZ = z;
        return true;
    }

    private void AdicionarEstadoTemporario(int lane, float ultimaAcaoZ) {
        for (int i = 0; i < estadosTemporarios.Count; i++) {
            if (estadosTemporarios[i].lane != lane)
                continue;

            if (ultimaAcaoZ < estadosTemporarios[i].ultimaAcaoZ)
                estadosTemporarios[i] = new EstadoPossivel(lane, ultimaAcaoZ);

            return;
        }

        estadosTemporarios.Add(new EstadoPossivel(lane, ultimaAcaoZ));
    }

    private void CopiarEstadosValidados() {
        estadosPossiveis.Clear();

        foreach (EstadoPossivel estado in estadosTemporarios)
            estadosPossiveis.Add(estado);
    }

    private void CriarLinha(TipoObstaculo[] linha, float z) {
        for (int i = 0; i < linha.Length; i++) {
            GameObject prefab = PrefabDoTipo(linha[i]);

            if (prefab == null)
                continue;

            int lane = IndexParaLane(i);
            Vector3 posicao = new Vector3(lane * laneWidth, spawnY, z);

            GameObject obstaculo = Instantiate(prefab, posicao, Quaternion.identity);
            obstaculosCriados.Add(obstaculo);
        }
    }

    private GameObject PrefabDoTipo(TipoObstaculo tipo) {
        return tipo switch {
            TipoObstaculo.Bloqueia => obstaculoBloqueiaPrefab,
            TipoObstaculo.Saltar => obstaculoSaltarPrefab,
            TipoObstaculo.Rolar => obstaculoRolarPrefab,
            _ => null
        };
    }

    private int MaxObstaculosPorLinha() {
        if (player.velocidadeAtual < 14f) return 1;
        if (player.velocidadeAtual < 19f) return 2;

        return 3;
    }

    private float DistanciaEntreLinhas() {
        return player.velocidadeAtual * tempoReacao + margemEntreLinhas;
    }

    private float DistanciaMinimaEntreAcoes() {
        return player.velocidadeAtual * tempoMinimoEntreAcoes;
    }

    private int LaneParaIndex(int lane) {
        return lane + 1;
    }

    private int IndexParaLane(int index) {
        return index - 1;
    }

    private void LimparObstaculosAntigos() {
        for (int i = obstaculosCriados.Count - 1; i >= 0; i--) {
            if (obstaculosCriados[i] == null) {
                obstaculosCriados.RemoveAt(i);
                continue;
            }

            if (obstaculosCriados[i].transform.position.z < playerTransform.position.z - distanciaParaDestruir) {
                Destroy(obstaculosCriados[i]);
                obstaculosCriados.RemoveAt(i);
            }
        }
    }

    public void AjustarOrigem(float deslocamentoZ) {
        proximoSpawnZ += deslocamentoZ;
        ultimoSpawnZ += deslocamentoZ;

        for (int i = obstaculosCriados.Count - 1; i >= 0; i--) {
            if (obstaculosCriados[i] == null) {
                obstaculosCriados.RemoveAt(i);
                continue;
            }

            Vector3 posicao = obstaculosCriados[i].transform.position;
            posicao.z += deslocamentoZ;
            obstaculosCriados[i].transform.position = posicao;
        }
    }
}
