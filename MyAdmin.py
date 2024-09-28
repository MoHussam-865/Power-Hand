import sqlite3

conn = sqlite3.connect('PowerHand.db')
cursor = conn.cursor()

cursor.execute('select name from sqlite_master where type="table";')

tables = cursor.fetchall()


for table in tables[1:]:

    cursor.execute(f'pragma table_info({table[0]});')
    columns = cursor.fetchall()
    
    c = []
    for col in columns:
        c.append(col[1])

    print(f'{table} : "{c}"')


def get_changes(i: int):
    cursor.execute(f'select * from Item where LastUpdate = {i}')
