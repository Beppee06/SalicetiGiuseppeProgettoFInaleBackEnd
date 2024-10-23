# Libreria Online - Progetto Finale

## Descrizione

Questo progetto consiste in un sistema completo di gestione di una libreria online. Gli utenti possono registrarsi, accedere, visualizzare un catalogo di libri, effettuare ordini e gestire il proprio profilo attraverso un'interfaccia API sicura protetta da JWT.

## Funzionalità

- **Gestione Clienti**: I clienti possono registrarsi e accedere utilizzando JSON Web Token (JWT).
- **Catalogo Libri**: Visualizzazione dei libri, ricerca per titolo o autore e possibilità di aggiungere nuovi libri.
- **Gestione Ordini**: Gli utenti possono effettuare ordini e visualizzare lo storico degli ordini.
- **Sicurezza**: Le API sono protette tramite autenticazione JWT.

## Tecnologie Utilizzate

- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT (JSON Web Tokens)

## Utilizzo dell'API

### Registrazione Utente

- **Endpoint**: `POST /User/Register`
- Inviare l'email e la password nel corpo della richiesta.

### Login Utente

- **Endpoint**: `POST /User/Login`
- Inviare l'email e la password nel corpo della richiesta.

### Visualizzazione Catalogo Libri

- **Endpoint**: `GET /Book/GetBookList`
- Autenticazione necessaria (includere il token JWT nell'header Authorization).

### Creazione Nuovo Libro

- **Endpoint**: `POST /Book/PostCreateBook`
- Inviare il titolo e l'autore nel corpo della richiesta.

### Effettuare un Ordine

- **Endpoint**: `POST /Book/PostOrder`
- Inviare il titolo e l'autore del libro nel corpo della richiesta.

