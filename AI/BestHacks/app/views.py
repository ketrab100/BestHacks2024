import pandas as pd
from rest_framework import viewsets
from rest_framework.decorators import action
from rest_framework.permissions import AllowAny
from rest_framework.response import Response
from sklearn.neighbors._regression import KNeighborsRegressor

from app.models import Matches, Tags, Usertags, Aspnetusers
from app.serializers import JobSerializer


class MatchViewSet(viewsets.ModelViewSet):
    queryset = Aspnetusers.objects.all()
    serializer_class = JobSerializer

    permission_classes = [AllowAny]

    @action(
        detail=False,
        methods=["get"],
        url_path="employers/(?P<pk>[^/.]+)",
    )
    def get_jobs_list(self, request, pk=None):
        tags = Tags.objects.all()
        matches = Matches.objects.filter(userid=pk).order_by('createdat')[:100].all()

        if matches.count() < 2:
            return Response([])

        tag_map = {tag.name: 1 for tag in tags}

        l = {}
        for t in tag_map:
            l[t] = []
            for m in matches:
                if t not in m.employerid.employertags.tagid.name.split('\n'):
                    l[t].append(0)
                else:
                    l[t].append(1)

        l["accepted"] = [m.didemployeeacceptjoboffer for m in matches]
        df = pd.DataFrame(l)
        x = df.drop("accepted", axis=1)
        x = x.values
        y = df["accepted"]
        y = y.values

        knn_model = KNeighborsRegressor(n_neighbors=2)
        knn_model.fit(x, y)

        user_tags = Usertags.objects.filter(userid=pk).all().select_related('tagid')
        tag_ids = user_tags.values_list('tagid', flat=True)
        employers = Aspnetusers.objects.filter(employertags__tagid__id__in=tag_ids)[:10]

        if employers.count() == 0:
            employers = Aspnetusers.objects.all()[:10]

        l = {}
        for e in employers:
            x = []
            for t in tag_map:
                try:
                    if t not in employers.employertags.tagid.name.split('\n'):
                        x.append(0)
                    else:
                        x.append(1)
                except:
                    x.append(0)
            l[e.id] = x

        x = [value for value in l.values()]

        x = [[0, 1], [0, 1]]

        train_preds = knn_model.predict(x)

        res = []
        for i, j in enumerate(employers):
            print(i)
            print(j)
            res.append((j.id, train_preds[i]))

        return Response(res)

    @action(
        detail=False,
        methods=["get"],
        url_path="employee",
    )
    def get_employees_list(self, request, pk=None):
        # tags = Jobs.objects.get(pk).tags.all()
        # matches = Matches.objects.filter(jobid=pk)

        return Response("asdasdasd")
