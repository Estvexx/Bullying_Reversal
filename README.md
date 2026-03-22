Bullying_Reversal

Bullying Reversal é um endless runner 3D onde és o miúdo novo que enfrentou o gunnão. Agora ele persegue-te no percurso casa-escola. Desvia-te de obstáculos e encontra amigos que criam barreiras temporárias para te ajudar a escapar. Desenvolvido em Unity para PC.

# Boas práticas de Git - Unity Project

## Branches

- `main` → versão estável, builds jogáveis
- `develop` → integração de features
- `feature/*` → cada funcionalidade nova, exemplo:
  - `feature/player-movement`
  - `feature/inventory-system`
  - `feature/test-feature`

## Workflow

1. Criar branch a partir de `develop`:
   ```bash
   git checkout develop
   git checkout -b feature/nome-da-feature
   ```
