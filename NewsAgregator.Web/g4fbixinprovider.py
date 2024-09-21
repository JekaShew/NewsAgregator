
import sys
from gpt4free.g4f.client import Client
from g4f.Provider import  Bixin123



def chat_with_model(message):
    client = Client(
        provider= Bixin123 
    )
    response = client.chat.completions.create(
        model="gpt-4-turbo",
        messages=[{"role": "user", "content": message}],
    )
    return print(response.choices[0].message.content)

if len(sys.argv) > 1:
    message = sys.argv[1]
    chat_with_model(message)
else:
        print("No message provided.")