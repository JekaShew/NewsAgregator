
import sys
from gpt4free.g4f.client import Client
from g4f.Provider import  Bing



def chat_with_model(message):
    client = Client(
        provider= Bing 
    )
    response = client.chat.completions.create(
        model="Copilot",
        messages=[{"role": "user", "content": message}],
    )
    return print(response.choices[0].message.content)

if len(sys.argv) > 1:
    message = sys.argv[1]
    chat_with_model(message)
else:
        print("No message provided.")