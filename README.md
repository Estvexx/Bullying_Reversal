# Bullying Reversal

Endless runner 3D desenvolvido em Unity como projeto escolar.

## Autor

| Nome                              | Numero de aluno |
| --------------------------------- | --------------- |
| Francisco Manuel Pinheiro Esteves | 32181           |

## Entrega

Este projeto corresponde a entrega da epoca de melhoria.

Tema escolhido: jogo 3D do tipo endless runner, com o tema "Bullying Reversal", onde o jogador foge de um perseguidor, evita obstaculos, recolhe livros e tenta sobreviver o maximo de tempo possivel.

## Versao do Unity

Unity 6000.3.9f1

## Descricao do jogo

Bullying Reversal e um endless runner 3D em que o jogador controla uma personagem que corre por um cenario continuo. O objetivo e sobreviver o maximo de tempo possivel, evitar obstaculos, recolher livros e aumentar a pontuacao.

O jogo inclui tambem um perseguidor que segue o jogador com atraso, replicando parte dos movimentos do player. A dificuldade nao depende apenas da velocidade: os obstaculos sao gerados proceduralmente com regras para evitar combinacoes impossiveis.

## Funcionalidades implementadas

- Movimento em 3 lanes: esquerda, centro e direita.
- Salto e roll para ultrapassar diferentes tipos de obstaculos.
- Obstaculos gerados de forma aleatoria com validacao de seguranca, evitando patterns impossiveis.
- Recolha de livros espalhados pelas lanes livres do mapa.
- Sistema de score com multiplicador progressivo.
- Highscore persistente atraves de PlayerPrefs.
- Game Over com pontuacao final, livros recolhidos e recorde.
- Menu principal com ultima pontuacao, recorde e total de livros.
- Sistema de definicoes para volume de musica, volume de efeitos e ativacao/desativacao de efeitos visuais.
- Multilinguagem: portugues e ingles.
- Perseguidor com replay de posicoes atraves de queue.
- Reducao da taxa de amostragem do perseguidor para melhorar performance em PCs mais fracos.
- Floating origin para evitar perda de precisao quando o player avanca muito no eixo Z.
- Colisao de esquina: o jogador pode continuar apos uma batida menos frontal.
- Feedback visual/camera shake quando o jogador bate de esquina.
- Persistencia de dados com PlayerPrefs.
- Som de salto, roll, recolha de livros e musica ambiente.

## Jogabilidade

### Objetivo

Sobreviver o maximo de tempo possivel, recolher livros e obter a maior pontuacao possivel sem colidir frontalmente com obstaculos.

### Controlos

| Tecla              | Acao                            |
| ------------------ | ------------------------------- |
| A ou seta esquerda | Mover para a lane da esquerda   |
| D ou seta direita  | Mover para a lane da direita    |
| W ou Space         | Saltar                          |
| S ou Left Shift    | Roll / deslizar                 |
| C                  | Trocar camera, quando aplicavel |

### Regras principais

- O jogo comeca apos a animacao inicial da personagem.
- A velocidade aumenta progressivamente ao longo do tempo.
- O multiplicador de score aumenta durante a run.
- Colisoes frontais terminam a partida.
- Colisoes de esquina podem permitir continuar, com feedback visual na camera.
- Os livros recolhidos contam para a run atual e para o total acumulado.
- O recorde fica guardado entre sessoes.

## Como abrir o projeto

1. Instalar o Unity Hub.
2. Instalar a versao Unity 6000.3.9f1.
3. Clonar ou descarregar este repositorio.
4. No Unity Hub, escolher Add e selecionar a pasta do projeto.
5. Abrir o projeto.
6. Abrir a cena Assets/Scenes/MenuPrincipal.unity.
7. Clicar em Play para correr o jogo no Editor.

As cenas incluidas no Build Settings sao:

- Assets/Scenes/MenuPrincipal.unity
- Assets/Scenes/Jogo.unity
- Assets/Scenes/Definicoes.unity

## Assets multimedia

### Modelos 3D e animacoes

- Modelos humanoides e animacoes provenientes de Mixamo/Adobe e assets gratuitos.
- Formatos usados principalmente FBX e assets nativos do Unity.
- Usados para personagem principal, perseguidor e personagem do ecra de Game Over.
- Animacoes usadas: entrada, corrida, salto, roll, morte, perseguicao e dancas de Game Over.

### Texturas e materiais

- Foram usados assets gratuitos da Unity Asset Store e texturas em formatos PNG e TGA.
- Algumas texturas de pacotes externos usam resolucoes altas, incluindo mapas de 2048 e 4096.
- As texturas maiores foram mantidas apenas quando necessarias para os materiais usados em cena.
- Existe trabalho de otimizacao possivel em texturas grandes de terceiros, especialmente quando nao sao usadas diretamente por cenas, prefabs ou materiais ativos.

### Sons

- Sons em formato WAV comprimido para Vorbis.
- Usados para salto, roll, recolha de livro e musica ambiente.

## Otimizacoes e qualidade tecnica

- Remocao de FindObjectOfType/FindFirstObjectByType nos principais scripts.
- Referencias configuradas por Inspector quando necessario.
- Uso de Animator.StringToHash para parametros do Animator.
- Tempos configuraveis no Inspector.
- SomManager com AudioSources separados para musica e efeitos.
- SomManager com DontDestroyOnLoad e protecao contra duplicados.
- Player e Inimigo comunicam por eventos, reduzindo acoplamento direto.
- PlayerHealth dispara eventos para score, livros e Game Over.
- Floating origin aplicado para controlar o crescimento do eixo Z.
- Obstaculos antigos do SpawnPlatform foram substituidos por ObstacleSpawner procedural.

## Observacoes e lacunas

- O projeto ainda pode beneficiar de uma limpeza final de assets grandes nao utilizados.
- Alguns assets de terceiros podem aumentar o tamanho do repositorio e o numero de batches.
- A geracao procedural dos obstaculos segue regras de seguranca, mas pode ser afinada com mais patterns no futuro.
- O sistema de linguas implementado cobre textos principais em portugues e ingles.
- O projeto nao usa namespaces nem assembly definitions, por decisao de manter a estrutura simples para um projeto individual pequeno.

## Demonstracao

Video de demonstracao:

https://youtu.be/qtlIHoXzD0U

Projeto desenvolvido para a unidade curricular de Tecnologias e Multimedia - 2025/2026.
