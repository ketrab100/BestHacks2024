from rest_framework import serializers

from app.models import Jobs


class JobSerializer(serializers.ModelSerializer):
    class Meta:
        model = Jobs
        fields = "__all__"
