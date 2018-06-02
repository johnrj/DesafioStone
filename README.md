# WebAPIMongoDBExample

Para configurar o MongoDB no seu ambiente, altere as chaves abaixo que estão no web.config do projeto WebAPIMongoDBExample:
```
<add key="BDUsuario" value="root"/>
<add key="BDSenha" value="123456"/>
<add key="BDHost" value="Morpheus"/>
<add key="BDPorta" value="27017"/>
```
### Features
1) Cadastro (CRUD) de Tipos de Imobilizado.
2) Cadastro (CRUD) de Imobilizados, que são os itens a serem emprestados
3) Cadastro (CRUD) de Utilização, que é o controle de uso de cada Imobilizado.
5) Verificação de disponibilizade para a data atual (http://localhost:51674/api/disponibilidade/)
6) Verificação de disponibilizade de uma data específica (http://localhost:51674/api/disponibilidade?d=2018-02-01)
7) Verificação de disponibilizade de um item e data específica (http://localhost:51674/api/disponibilidade?id=5a63b3f7a872872ce8b6235f&d=2018-02-01)
