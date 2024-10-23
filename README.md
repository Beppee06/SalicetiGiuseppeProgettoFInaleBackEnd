# Libreria Online - Progetto Finale

ASP.NET Core
============

[![.NET Foundation](https://img.shields.io/badge/.NET%20Foundation-blueviolet.svg)](https://www.dotnetfoundation.org/)
[![MIT License](https://img.shields.io/github/license/dotnet/aspnetcore?color=%230b0&style=flat-square)](https://github.com/dotnet/aspnetcore/blob/main/LICENSE.txt) [![Help Wanted](https://img.shields.io/github/issues/dotnet/aspnetcore/help%20wanted?color=%232EA043&label=help%20wanted&style=flat-square)](https://github.com/dotnet/aspnetcore/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22) [![Good First Issues](https://img.shields.io/github/issues/dotnet/aspnetcore/good%20first%20issue?color=%23512BD4&label=good%20first%20issue&style=flat-square)](https://github.com/dotnet/aspnetcore/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22)
[![Discord](https://img.shields.io/discord/732297728826277939?style=flat-square&label=Discord&logo=discord&logoColor=white&color=7289DA)](https://aka.ms/dotnet-discord)

ASP.NET Core is an open-source and cross-platform framework for building modern cloud-based internet-connected applications, such as web apps, IoT apps, and mobile backends. ASP.NET Core apps run on [.NET](https://dot.net), a free, cross-platform, and open-source application runtime. It was architected to provide an optimized development framework for apps that are deployed to the cloud or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your solutions. You can develop and run your ASP.NET Core apps cross-platform on Windows, Mac, and Linux. [Learn more about ASP.NET Core](https://learn.microsoft.com/aspnet/core/).

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

