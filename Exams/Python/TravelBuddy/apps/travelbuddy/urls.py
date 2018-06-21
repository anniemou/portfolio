from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^register/', views.register),
    url(r'^login/', views.login),
    url(r'^logout/', views.logout),
    url(r'^travelplans/', views.travelplans),
    url(r'^addplan/', views.addplan),
    url(r'^createplan/', views.createplan),
    url(r'^join/(?P<trip_id>\d+)$', views.join),
    url(r'^show/(?P<trip_id>\d+)$', views.show),
    url(r'^delete/(?P<trip_id>\d+)$', views.delete),
    ]