# 🏃 Bullying Reversal

> Endless Runner 3D desenvolvido em Unity como projeto escolar.

---

## 👤 Autor

| Nome | Número de Aluno |
|------|----------------|
| Francisco Manuel Pinheiro Esteves | 32181 |

---

## 🛠️ Versão do Unity

**Unity 6000.3.9f1**

---

## 📖 Descrição do Jogo

**Bullying Reversal** é um jogo de endless runner 3D em que o jogador controla um personagem que corre infinitamente por um cenário, desviando de obstáculos, recolhendo livros e tentando sobreviver o máximo de tempo possível — enquanto um perseguidor corre atrás de si com um ligeiro atraso.

### Funcionalidades Implementadas

- **Corrida infinita** com aumento progressivo de velocidade ao longo do tempo
- **Sistema de lanes** — o jogador pode mover-se entre 3 faixas (esquerda, centro, direita)
- **Salto e Roll** — mecânicas para evitar obstáculos
- **Sistema de score** com multiplicador crescente ao longo do tempo
- **Recolha de livros** com efeito de partículas e som
- **Inimigo perseguidor** que replica os movimentos do jogador com delay
- **Ecrã de Game Over** com personagem 3D animado que dança consoante a pontuação obtida
- **Menu Principal** com visualização da última pontuação, recorde e total de livros recolhidos
- **Sistema de definições** — controlo de volume de música, volume de sons e toggle de efeitos visuais
- **Persistência de dados** com PlayerPrefs (última pontuação, recorde, livros totais)
- **Efeitos de partículas** — poeira nos pés durante a corrida e explosão ao recolher livros
- **Animação de entrada** — personagem faz uma dança inicial antes de começar a correr
- **Botão de eliminar dados** guardados em memória

---

## 🎮 Jogabilidade

### Objetivo
Correr o máximo de tempo possível, recolher livros e acumular pontuação sem colidir com obstáculos.

### Controlos

| Tecla | Ação |
|-------|------|
| `A` ou `←` | Mover para a lane da esquerda |
| `D` ou `→` | Mover para a lane da direita |
| `W` ou `Space` | Saltar |
| `S` ou `Left Shift` | Roll (deslizar) |

### Regras
- O jogo começa automaticamente após a animação de entrada do personagem
- A velocidade aumenta progressivamente ao longo do tempo
- O multiplicador de pontuação aumenta a cada 10 segundos
- Colidir com um obstáculo termina o jogo
- Os livros recolhidos são acumulados no total global entre sessões
- Consoante a pontuação final, o personagem no ecrã de Game Over executa uma dança diferente

---

## 📂 Como Abrir o Projeto

1. Instalar o **Unity Hub** em [unity.com](https://unity.com/download)
2. Instalar a versão **Unity 6000.3.9f1** através do Unity Hub
3. Clonar ou descarregar este repositório
4. No Unity Hub, clicar em **"Add"** e selecionar a pasta do projeto
5. Abrir o projeto e carregar a cena **`MenuPrincipal`**
6. Clicar em **Play** para testar o jogo no editor

---

## 🗂️ Assets Multimédia

### 🎵 Sons
- **Formato:** WAV
- **Fonte:** [freesound.org](https://freesound.org) — sons gratuitos e livres de direitos
- **Sons implementados:** som de salto, som de roll, som de recolha de livro e música ambiente em loop
- **Justificação:** o formato WAV garante boa qualidade de áudio sem compressão percetível, adequado para efeitos sonoros curtos e música de fundo

### 🧍 Modelos 3D
- **Fonte:** [Mixamo (Adobe)](https://www.mixamo.com) — modelos e animações gratuitos
- **Modelos usados:** personagem principal, personagem perseguidor e personagem do ecrã de Game Over
- **Animações:** corrida, salto, roll, corrida rápida, danças e animação de entrada
- **Justificação:** o Mixamo oferece modelos humanoides com animações de alta qualidade prontos a usar no Unity, poupando tempo de produção

### 🎨 Texturas e Cenário
- **Fonte:** Unity Asset Store — assets gratuitos
- **Justificação:** os assets do Unity Asset Store garantem compatibilidade nativa com o motor, qualidade visual consistente e licença de uso em projetos académicos
- **Nota:** embora os assets sejam de terceiros, o cenário foi **montado e ajustado manualmente peça por peça**, incluindo disposição dos elementos, escala e iluminação

---

## 📋 Observações

- O volume da música e dos sons é guardado entre sessões via PlayerPrefs
- Os efeitos visuais de partículas podem ser desativados nas definições
- O botão vermelho (🗑️) no menu principal elimina todos os dados guardados (pontuação, recorde e livros)
- O perseguidor só aparece na cena quando o personagem principal começa a correr, para não interferir com a animação de entrada
- O personagem no ecrã de Game Over é independente do personagem principal e executa animações diferentes consoante a pontuação:
  - Score ≤ 1000 → Dança 1
  - Score entre 1000 e 2000 → Dança 2
  - Score > 2000 → Dança 3

---

## 🎥 Demonstração (Secção para demonstração não realizada em contexto de sala de aula)

> Demonstração do inimigo perseguidor a replicar os movimentos do jogador com delay, assim como um pouco do jogo.
[▶️ Ver vídeo no YouTube](https://youtu.be/qtlIHoXzD0U)

*Projeto desenvolvido para a unidade curricular de Tecnoligas e Multimédia — 2025/2026*
