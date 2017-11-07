import sys
with file(sys.argv[1], 'r') as original: data = original.read()
with file(sys.argv[2], 'w') as modified: modified.write("sep=,\n" + data)