import asyncio
import websockets
import json
import sqlite3

# Skapa eller anslut till en SQLite-databas
conn = sqlite3.connect('crypto_prices.db')
c = conn.cursor()

# Skapa en tabell om den inte redan finns
c.execute('''CREATE TABLE IF NOT EXISTS prices
             (symbol TEXT, price REAL, timestamp DATETIME DEFAULT CURRENT_TIMESTAMP)''')
conn.commit()

async def get_binance_price(symbol):
    uri = f"wss://stream.binance.com:9443/ws/{symbol.lower()}@miniTicker"

    async with websockets.connect(uri) as websocket:
        while True:
            response = await websocket.recv()
            data = json.loads(response)
            price = data['c']
            print(f"Pris {symbol.upper()}: {price} USD")

            # Spara priset i databasen
            c.execute("INSERT INTO prices (symbol, price) VALUES (?, ?)", (symbol.upper(), price))
            conn.commit()

            await asyncio.sleep(1)

if __name__ == "__main__":
    symbol = "btcusdt"
    try:
        asyncio.get_event_loop().run_until_complete(get_binance_price(symbol))
    except KeyboardInterrupt:
        pass
    finally:
        conn.close()
