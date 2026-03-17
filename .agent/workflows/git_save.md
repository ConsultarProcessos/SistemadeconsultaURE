---
description: Como salvar e enviar alterações para o GitLab (Git)
---

Siga estes passos sempre que fizer uma alteração manual no código e quiser que ela apareça no site (GitLab Pages):

1. **Salvar o arquivo**: No seu editor de código, aperte `Ctrl + S` para garantir que a alteração foi salva no computador.
2. **Abrir o Terminal**: Vá na pasta do projeto (`Integracao_Suzano_V2`).
3. **Adicionar as mudanças**:
```bash
git add .
```
4. **Criar um comentário**:
```bash
git commit -m "Descreva aqui o que você mudou"
```
5. **Enviar para o GitLab**:
// turbo
```bash
git push origin v2-portal
```

> [!IMPORTANT]
> O seu site está configurado para atualizar apenas quando você envia para a branch **v2-portal**. Se você enviar para outra branch, o site não atualizará sozinho.
