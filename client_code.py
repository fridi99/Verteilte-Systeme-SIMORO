# took this code as a framework from:
# https://www.rabbitmq.com/tutorials/tutorial-three-python

#!/usr/bin/env python
import random
import pika
import sys
import time

while True:
    connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()
    message = str(random.randint(0,100))
    channel.exchange_declare(exchange='data', exchange_type='fanout')
    channel.basic_publish(exchange='data', routing_key='', body=message)
    print(f" [x] Sent {message}")
    connection.close()
    time.sleep(1)