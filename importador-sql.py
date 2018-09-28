#!python3
import csv
import sys
filename = 'Planilha-Testecomdificuldades.csv'

subjectId = 2
moduleId = 2
topicId = 1

sql = '''INSERT INTO `projeto`.`Subject` (`Id`, `Description`, `Name`)
    VALUES ( {:d} , 'Descrição da Matéria' , 'Ficar Grandao' );'''.format(subjectId)

sql = sql + '''INSERT INTO `projeto`.`Module`(`Id`, `Active`, `Description`, `Name`, `SubjectId`)
    VALUES ({:d}, 1, 'Modulo 1', 'Modulo 1', {:d}); '''.format(moduleId, subjectId)

sql = sql + '''INSERT INTO `projeto`.`Topic` (`Id`, `Active`, `Description`, `Difficulty`, `ModuleId`, `Name`)
    VALUES ({:d}, 1, 'Proteinas', 1, {:d}, 'Proteinas'); '''.format(topicId, moduleId)

optionMap = {
    'a': 1,
    'b': 2,
    'c': 3,
    'd': 4,
    'A': 1,
    'B': 2,
    'C': 3,
    'D': 4,
    '': 0
}

with open(filename, 'rt') as f:
    reader = csv.reader(f)
    try:
        QuestionId = 0
        for row in reader:
            QuestionId = QuestionId + 1

            #pergunta
            sql = sql + '''INSERT INTO `projeto`.`Question` (`Id`, `Active`, `Description`, `Difficulty`, `Hint`, `TopicId`)
    VALUES({:d}, 1, "{:s}", {:d}, null, {:d}); '''.format(QuestionId, row[0], int(row[6]) if row[6] != "" else 1, topicId)

            #row[0] => pergunta

            #row[1] => resposta
            #row[2] => resposta
            #row[3] => resposta
            #row[4] => resposta

            #row[5] => resposta certa

            #row[6] => dificuldade

            for resposta in range(1,5):
                if row[resposta] != "":
                    sql = sql + '''INSERT INTO `projeto`.`Answer`(`Correct`,`Description`,`QuestionId`)
        VALUES({:d},'{:s}',{:d});
                        '''.format(1 if resposta == optionMap[row[5]] else 0, row[resposta], QuestionId)

        print("Ok")
    except:
        print("Deu Ruim")
        pass

file = open('sql.sql', "w")

print(sql, file=file)

file.close()
