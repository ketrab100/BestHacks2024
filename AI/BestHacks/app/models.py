# This is an auto-generated Django model module.
# You'll have to do the following manually to clean this up:
#   * Rearrange models' order
#   * Make sure each model has one field with primary_key=True
#   * Make sure each ForeignKey and OneToOneField has `on_delete` set to the desired behavior
#   * Remove `managed = False` lines if you wish to allow Django to create, modify, and delete the table
# Feel free to rename the models, but don't rename db_table values or field names.
from django.db import models


class Aspnetroleclaims(models.Model):
    id = models.AutoField(db_column='Id', primary_key=True)  # Field name made lowercase.
    roleid = models.ForeignKey('Aspnetroles', models.DO_NOTHING, db_column='RoleId')  # Field name made lowercase.
    claimtype = models.TextField(db_column='ClaimType', blank=True, null=True)  # Field name made lowercase.
    claimvalue = models.TextField(db_column='ClaimValue', blank=True, null=True)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetRoleClaims'


class Aspnetroles(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    name = models.CharField(db_column='Name', max_length=256, blank=True, null=True)  # Field name made lowercase.
    normalizedname = models.CharField(db_column='NormalizedName', unique=True, max_length=256, blank=True,
                                      null=True)  # Field name made lowercase.
    concurrencystamp = models.TextField(db_column='ConcurrencyStamp', blank=True,
                                        null=True)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetRoles'


class Aspnetuserclaims(models.Model):
    id = models.AutoField(db_column='Id', primary_key=True)  # Field name made lowercase.
    userid = models.ForeignKey('Aspnetusers', models.DO_NOTHING, db_column='UserId')  # Field name made lowercase.
    claimtype = models.TextField(db_column='ClaimType', blank=True, null=True)  # Field name made lowercase.
    claimvalue = models.TextField(db_column='ClaimValue', blank=True, null=True)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetUserClaims'


class Aspnetuserlogins(models.Model):
    loginprovider = models.TextField(db_column='LoginProvider',
                                     primary_key=True)  # Field name made lowercase. The composite primary key (LoginProvider, ProviderKey) found, that is not supported. The first column is selected.
    providerkey = models.TextField(db_column='ProviderKey')  # Field name made lowercase.
    providerdisplayname = models.TextField(db_column='ProviderDisplayName', blank=True,
                                           null=True)  # Field name made lowercase.
    userid = models.ForeignKey('Aspnetusers', models.DO_NOTHING, db_column='UserId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetUserLogins'
        unique_together = (('loginprovider', 'providerkey'),)


class Aspnetuserroles(models.Model):
    userid = models.OneToOneField('Aspnetusers', models.DO_NOTHING, db_column='UserId',
                                  primary_key=True)  # Field name made lowercase. The composite primary key (UserId, RoleId) found, that is not supported. The first column is selected.
    roleid = models.ForeignKey(Aspnetroles, models.DO_NOTHING, db_column='RoleId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetUserRoles'
        unique_together = (('userid', 'roleid'),)


class Aspnetusertokens(models.Model):
    userid = models.OneToOneField('Aspnetusers', models.DO_NOTHING, db_column='UserId',
                                  primary_key=True)  # Field name made lowercase. The composite primary key (UserId, LoginProvider, Name) found, that is not supported. The first column is selected.
    loginprovider = models.TextField(db_column='LoginProvider')  # Field name made lowercase.
    name = models.TextField(db_column='Name')  # Field name made lowercase.
    value = models.TextField(db_column='Value', blank=True, null=True)  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'AspNetUserTokens'
        unique_together = (('userid', 'loginprovider', 'name'),)


class Aspnetusers(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
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
    bio = models.TextField(db_column='Bio', blank=True, null=True)  # Field name made lowercase.
    companyname = models.TextField(db_column='CompanyName', blank=True, null=True)  # Field name made lowercase.
    contactname = models.TextField(db_column='ContactName', blank=True, null=True)  # Field name made lowercase.
    createdat = models.DateTimeField(db_column='CreatedAt', blank=True, null=True)  # Field name made lowercase.
    discriminator = models.CharField(db_column='Discriminator', max_length=8)  # Field name made lowercase.
    employee_createdat = models.DateTimeField(db_column='Employee_CreatedAt', blank=True,
                                              null=True)  # Field name made lowercase.
    employee_location = models.TextField(db_column='Employee_Location', blank=True,
                                         null=True)  # Field name made lowercase.
    experiencelevel = models.TextField(db_column='ExperienceLevel', blank=True, null=True)  # Field name made lowercase.
    firstname = models.TextField(db_column='FirstName', blank=True, null=True)  # Field name made lowercase.
    lastname = models.TextField(db_column='LastName', blank=True, null=True)  # Field name made lowercase.
    location = models.TextField(db_column='Location', blank=True, null=True)  # Field name made lowercase.

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


class Jobtags(models.Model):
    jobid = models.OneToOneField('Jobs', models.DO_NOTHING, db_column='JobId',
                                 primary_key=True)  # Field name made lowercase. The composite primary key (JobId, TagId) found, that is not supported. The first column is selected.
    tagid = models.ForeignKey('Tags', models.DO_NOTHING, db_column='TagId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'JobTags'
        unique_together = (('jobid', 'tagid'),)


class Jobs(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    jobtitle = models.TextField(db_column='JobTitle')  # Field name made lowercase.
    jobdescription = models.TextField(db_column='JobDescription')  # Field name made lowercase.
    location = models.TextField(db_column='Location')  # Field name made lowercase.
    experiencelevel = models.TextField(db_column='ExperienceLevel')  # Field name made lowercase.
    createdat = models.DateTimeField(db_column='CreatedAt')  # Field name made lowercase.
    employerid = models.ForeignKey(Aspnetusers, models.DO_NOTHING, db_column='EmployerId')  # Field name made lowercase.

    class Meta:
        managed = False
        db_table = 'Jobs'


class Matches(models.Model):
    id = models.UUIDField(db_column='Id', primary_key=True)  # Field name made lowercase.
    matchscore = models.DecimalField(db_column='MatchScore', max_digits=65535,
                                     decimal_places=65535)  # Field name made lowercase.
    createdat = models.DateTimeField(db_column='CreatedAt')  # Field name made lowercase.
    userid = models.ForeignKey(Aspnetusers, models.DO_NOTHING, db_column='UserId')  # Field name made lowercase.
    jobid = models.ForeignKey(Jobs, models.DO_NOTHING, db_column='JobId')  # Field name made lowercase.

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
