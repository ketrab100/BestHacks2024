import pandas as pd
from rest_framework import viewsets
from rest_framework.decorators import action
from rest_framework.permissions import AllowAny
from rest_framework.response import Response
from sklearn.neighbors._regression import KNeighborsRegressor

from app.models import Jobs, Matches, Tags, Usertags
from app.serializers import JobSerializer


class MatchViewSet(viewsets.ModelViewSet):
    queryset = Jobs.objects.all()
    serializer_class = JobSerializer

    permission_classes = [AllowAny]

    @action(
        detail=False,
        methods=["get"],
        url_path="job/(?P<pk>[^/.]+)",
    )
    def get_jobs_list(self, request, pk=None):
        tags = Tags.objects.all()
        matches = Matches.objects.filter(userid=pk).order_by('createdat')[:100].all()

        tag_map = {tag.name: 1 for tag in tags}
        l = {}
        for t in tag_map:
            l[t] = []
            for m in matches:
                if t not in m.jobid.jobtags.tagid.name.split('\n'):
                    l[t].append(0)
                else:
                    l[t].append(1)

        l["accepted"] = [m.matchscore for m in matches]

        df = pd.DataFrame(l)
        x = df.drop("accepted", axis=1)
        x = x.values
        y = df["accepted"]
        y = y.values
        knn_model = KNeighborsRegressor(n_neighbors=2)
        knn_model.fit(x, y)

        x_train = [[1, 0, 0, 0, 0, 0, 0], [0, 1, 0, 0, 0, 0, 1]]

        user_tags = Usertags.objects.filter(userid=pk).all().select_related('tagid')
        tag_ids = user_tags.values_list('tagid', flat=True)
        jobs = Jobs.objects.filter(jobtags__tagid__id__in=tag_ids)[:10]
        if jobs.count() == 0:
            jobs = Jobs.objects.all()[:10]

        l = {}
        for j in jobs:
            x = []
            for t in tag_map:
                try:
                    if t not in j.jobtags.tagid.name.split('\n'):
                        x.append(0)
                    else:
                        x.append(1)
                except:
                    x.append(0)
            l[j.id] = x

        x = [value for value in l.values()]

        train_preds = knn_model.predict(x)

        res = []
        for i, j in enumerate(jobs):
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
        tags = Jobs.objects.get(pk).tags.all()
        matches = Matches.objects.filter(jobid=pk)

        return Response("asdasdasd")
