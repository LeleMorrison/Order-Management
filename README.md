
## Requisiti

- .NET 8 SDK (o superiore) installato.
- Un editor come Visual Studio 2022 (o VS Code) per eseguire la soluzione.
- (Opzionale) Git, se si vuole clonare il progetto da un repository.

## Istruzioni per l'installazione e l'avvio

1. **Clonazione del repository:**  
   Esegui il comando seguente per clonare il progetto da GitHub:

     ```bash
     git clone https://github.com/LeleMorrison/GestioneOrdini-PhotoSi.git
     ```



## Ripristino dei pacchetti e build:
Apri la soluzione in Visual Studio oppure, da linea di comando, posizionati nella cartella del progetto ed esegui:
```bash
dotnet restore
dotnet build
```
Questo comando ripristinerà tutti i pacchetti NuGet necessari (ad esempio, Entity Framework Core per SQLite e InMemory, xUnit etc.) e compilerà tutti i progetti.

## Test ed esecuzione
Aprire visual studio e procedere nel menù test o premere **CRTL** + **R**+**A** e cliccare il tasto verde per procedere nei test.


![immagine](https://github.com/user-attachments/assets/7c0e578c-fafc-4f8e-9c96-e538e91cb24b)

In alternativa si possono testare le chiamate con **PostMan**.

## Metodo Postman
Avviare visual studio e avviare tutti i microservizi.
Esempio creare un ordine con postman, inserire l'header **Content-Type** con value **application/json** dove item è un array di prodotti.

```json
{
  "userId": 1,
  "addressId": 2,
  "items": [
    {
      "productId": 17,
      "quantity": 2
    },
        {
      "productId": 18,
      "quantity": 2
    }
  ]
}
```
Se i servizi sono stati correttamente avviati riceveremo codice **201** da parte di postman con questa response:
```json
{
    "id": 6,
    "userId": 1,
    "addressId": 2,
    "orderDate": "2025-03-03T22:13:59.9211812Z",
    "items": [
        {
            "id": 14,
            "orderId": 6,
            "productId": 17,
            "quantity": 2
        },
        {
            "id": 15,
            "orderId": 6,
            "productId": 18,
            "quantity": 2
        }
    ]
}
```

