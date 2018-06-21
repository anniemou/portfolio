# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.shortcuts import render, HttpResponse, redirect
from .models import *
import bcrypt

# Create your views here.

def index(request):
    if 'logged' in request.session:
        if request.session['logged']:
            return redirect('/travelplans')

    return render(request, "travelbuddy/index.html")

def logout(request):
    request.session.clear()
    return redirect("/")

def register(request):
    username = request.POST['username']

    # O xrhsths
    user = User()

    if len(User.objects.filter(username = username)) > 0:
        print "User already exists with this username"
        return redirect("/")

    # Diabazw to password kryptografimeno
    request.session['message'] = "registered"

    hashed = bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt())
    user = User.objects.create(fullname = request.POST['fullname'], username = request.POST['username'], password = hashed)
    request.session['logged'] = True
    request.session['userid'] = user.id

    return redirect("/travelplans")

def login(request):
    username = request.POST['username']
    password = request.POST['password']

    if len(User.objects.filter(username = username)) == 0:
        print "User doesn't exist, can't login."
        return redirect("/")

    user = User.objects.get(username = username)

    if bcrypt.checkpw(password.encode(), user.password.encode()):
        request.session['message'] = "logged in"
        
        request.session['userid'] = user.id
        request.session['logged'] = True
    
        return redirect("/travelplans")

    return redirect("/")

def travelplans(request):
    # If not logged in, go to index
    if not request.session['logged']:
        return redirect("/")
    user = User.objects.get(id=request.session['userid'])
    context = {
        "user": user,
        "trips" : Travel.objects.all(),
        "others": Travel.objects.all().exclude(creator__id = user.id).exclude(join= user)
    }
    
    return render(request, "travelbuddy/travelplans.html", context = context)

def addplan(request):
    # If not logged in, go to index
    if not request.session['logged']:
        return redirect("/")

    if request.method != 'POST':
        return redirect ("/createplan")

    plan = Travel.objects.create(destination=request.POST['destination'],description=request.POST['description'], datestart=request.POST['datestart'],dateend=request.POST['dateend'], creator = User.objects.get(id=request.session['userid']))

    return redirect('/travelplans')


def createplan(request):
    # If not logged in, go to index
    if not request.session['logged']:
        return redirect("/")
    
    return render(request, "travelbuddy/createplan.html")


def join(request, trip_id):
    # If not logged in, go to index
    if not request.session['logged']:
        return redirect("/")
        
    if request.method == "GET":
        return redirect('/')
    
    user = User.objects.get(id = request.session["userid"])
    trip = Travel.objects.get(id = trip_id)
    trip.join.add(user)

    return redirect('/travelplans')

def show(request, trip_id):
    # If not logged in, go to index
    if not request.session['logged']:
        return redirect("/")
    
    travel= Travel.objects.get(id=trip_id)

    context={
        "travel": travel,
        "user":User.objects.get(id=request.session['userid']),
        "others": User.objects.filter(partymembers__id = travel.id).exclude(id=travel.creator.id),
    }

    return render(request, 'travelbuddy/show.html', context)

def delete(request, trip_id):
    # If not logged in, go to index
    if not request.session['logged']:
        return redirect("/")
    
    Travel.objects.get(id=trip_id).delete()

    return redirect('/travelplans')