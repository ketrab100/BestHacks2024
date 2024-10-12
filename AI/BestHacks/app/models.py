from django.db import models


class Aspnetusers(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    discriminator = models.CharField(db_column='Discriminator', max_length=8)  # Field name made lowercase.
    firstname = models.TextField(db_column='FirstName', blank=True, null=True)  # Field name made lowercase.
    lastname = models.TextField(db_column='LastName', blank=True, null=True)  # Field name made lowercase.
    bio = models.TextField(db_column='Bio', blank=True, null=True)  # Field name made lowercase.
    employee_location = models.TextField(db_column='Employee_Location', blank=True,
                                         null=True)  # Field name made lowercase.
    employee_experiencelevel = models.TextField(db_column='Employee_ExperienceLevel', blank=True,
                                                null=True)  # Field name made lowercase.
    employee_createdat = models.DateTimeField(db_column='Employee_CreatedAt', blank=True,
                                              null=True)  # Field name made lowercase.
    companyname = models.TextField(db_column='CompanyName', blank=True, null=True)  # Field name made lowercase.
    contactname = models.TextField(db_column='ContactName', blank=True, null=True)  # Field name made lowercase.
    createdat = models.DateTimeField(db_column='CreatedAt', blank=True, null=True)  # Field name made lowercase.
    jobtitle = models.TextField(db_column='JobTitle', blank=True, null=True)  # Field name made lowercase.
    jobdescription = models.TextField(db_column='JobDescription', blank=True, null=True)  # Field name made lowercase.
    location = models.TextField(db_column='Location', blank=True, null=True)  # Field name made lowercase.
    experiencelevel = models.TextField(db_column='ExperienceLevel', blank=True, null=True)  # Field name made lowercase.
    tagid = models.ForeignKey('Tags', models.DO_NOTHING, db_column='TagId', blank=True,
                              null=True)  # Field name made lowercase.
    username = models.CharField(db_column='UserName', max_length=256, blank=True,
                                null=True)  # Field name made lowercase.
    normalizedusername = models.CharField(db_column='NormalizedUserName', unique=True, max_length=256, blank=True,
                                          null=True)  # Field name made lowercase.
    email = models.CharField(db_column='Email', max_length=256, blank=True, null=True)  # Field name made lowercase.
    normalizedemail = models.CharField(db_column='NormalizedEmail', max_length=256, blank=True,
                                       null=True)  # Field name made lowercase.
    emailconfirmed = models.BooleanField(db_column='EmailConfirmed')  # Field name made lowercase.
    passwordhash = models.TextField(db_column='PasswordHash', blank=True, null=True)  # Field name made lowercase.
    securitystamp = models.TextField(db_column='SecurityStamp', blank=True, null=True)  # Field name made lowercase.
    concurrencystamp = models.TextField(db_column='ConcurrencyStamp', blank=True,
                                        null=True)  # Field name made lowercase.
    phonenumber = models.TextField(db_column='PhoneNumber', blank=True, null=True)  # Field name made lowercase.
    phonenumberconfirmed = models.BooleanField(db_column='PhoneNumberConfirmed')  # Field name made lowercase.
    twofactorenabled = models.BooleanField(db_column='TwoFactorEnabled')  # Field name made lowercase.
    lockoutend = models.DateTimeField(db_column='LockoutEnd', blank=True, null=True)  # Field name made lowercase.
    lockoutenabled = models.BooleanField(db_column='LockoutEnabled')  # Field name made lowercase.
    accessfailedcount = models.IntegerField(db_column='AccessFailedCount')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetUsers'


class Conversations(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    message = models.TextField(db_column='Message')  # Field name made lowercase.
    createdat = models.DateTimeField(db_column='CreatedAt')  # Field name made lowercase.
    matchid = models.ForeignKey('Matches', models.DO_NOTHING, db_column='MatchId')  # Field name made lowercase.
    senderid = models.ForeignKey(Aspnetusers, models.DO_NOTHING, db_column='SenderId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'Conversations'


class Employertags(models.Model):
    employerid = models.OneToOneField(Aspnetusers, models.DO_NOTHING, db_column='EmployerId',
                                      primary_key=True)  # Field name made lowercase. The composite primary key (EmployerId, TagId) found, that is not supported. The first column is selected.
    tagid = models.ForeignKey('Tags', models.DO_NOTHING, db_column='TagId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'EmployerTags'
        unique_together = (('employerid', 'tagid'),)


class Matches(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    didemployeeacceptjoboffer = models.BooleanField(db_column='DidEmployeeAcceptJobOffer')  # Field name made lowercase.
    didemployeracceptcandidate = models.BooleanField(
        db_column='DidEmployerAcceptCandidate')  # Field name made lowercase.
    arematched = models.BooleanField(db_column='AreMatched')  # Field name made lowercase.
    createdat = models.DateTimeField(db_column='CreatedAt')  # Field name made lowercase.
    userid = models.ForeignKey(Aspnetusers, models.DO_NOTHING, db_column='UserId')  # Field name made lowercase.
    jobid = models.UUIDField(db_column='JobId')  # Field name made lowercase.
    employerid = models.ForeignKey(Aspnetusers, models.DO_NOTHING, db_column='EmployerId',
                                   related_name='matches_employerid_set')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'Matches'


class Tags(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    name = models.TextField(db_column='Name')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'Tags'


class Usertags(models.Model):
    userid = models.OneToOneField(Aspnetusers, models.DO_NOTHING, db_column='UserId',
                                  primary_key=True)  # Field name made lowercase. The composite primary key (UserId, TagId) found, that is not supported. The first column is selected.
    tagid = models.ForeignKey(Tags, models.DO_NOTHING, db_column='TagId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'UserTags'
        unique_together = (('userid', 'tagid'),)


class Efmigrationshistory(models.Model):
    migrationid = models.CharField(db_column='MigrationId', primary_key=True,
                                   max_length=150)  # Field name made lowercase.
    productversion = models.CharField(db_column='ProductVersion', max_length=32)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = '__EFMigrationsHistory'
