
import sys
from gpt4free.g4f.client import Client
from g4f.Provider import  Blackbox



def chat_with_model(message):
    client = Client(
        provider= Blackbox 
    )
    response = client.chat.completions.create(
        model="gemini-1.5-flash",
        messages=[{"role": "user", "content": message}],
    )
    return print(response.choices[0].message.content)

if len(sys.argv) > 1:
    message = sys.argv[1]
    chat_with_model(message)
else:
        print("No message provided.")