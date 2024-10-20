from rest_framework import serializers

from app.models import Aspnetusers


class JobSerializer(serializers.ModelSerializer):
    class Meta:
        model = Aspnetusers
        fields = "__all__"
