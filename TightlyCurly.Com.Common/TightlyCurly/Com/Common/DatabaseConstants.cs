namespace TightlyCurly.Com.Common;

public static class DatabaseConstants
{
    public static class DatabaseNames
    {
        public const string DefaultDatabase = "TightlyCurly";
    }

    public static class ConnectionStringNames
    {
        public const string TightlyCurly = "TightlyCurly";
    }

    public static class StoredProcedures
    {
        public static class Parameters
        {
            public const string NameIn = "@name";

            public const string FirstNameIn = "@firstName";

            public const string LastNameIn = "@lastName";

            public const string DescriptionIn = "@description";

            public const string EmailAddressIn = "@emailAddress";

            public const string PreferredFormatIn = "@preferredFormat";

            public const string PreferredLanguageIn = "@preferredLanguage";

            public const string LocaleIdIn = "@localeId";

            public const string LcidIn = "@lcid";

            public const string LocaleNameIn = "@localeName";

            public const string KeyIn = "@key";

            public const string ValueIn = "@value";

            public const string MessageTextIn = "@messageText";

            public const string MessageSubjectIn = "@messageSubject";

            public const string TypeIdIn = "@typeId";

            public const string IngredientIdIn = "@ingredientId";

            public const string TitleIn = "@title";

            public const string AliasIn = "@alias";

            public const string InternalLinksIn = "@internalLinks";

            public const string ExternalLinksIn = "@externalLinks";

            public const string CategoryIn = "@category";

            public const string IngredientRatingIn = "@ingredientRating";

            public const string QuestionIdIn = "@questionId";

            public const string QuestionIn = "@question";

            public const string AnswerIn = "@answer";

            public const string QuestionCategoryIdIn = "@questionCategoryId";

            public const string QuestionCategoryIdsIn = "@questionCategoryIds";

            public const string ParentCategoryIdIn = "@parentCategoryId";

            public const string CriteriaIn = "@criteria";

            public const string SettingIdIn = "@settingId";

            public const string IsActiveIn = "@isActive";

            public const string IsPrivateIn = "@isPrivate";

            public const string CampaignNameIn = "@campaignName";

            public const string PageIdIn = "@pageId";

            public const string PageContentIdIn = "@pageContentId";

            public const string ContentIn = "@content";

            public const string ViewStatus = "@viewStatus";

            public const string MetaDescriptionIn = "@metaDescription";

            public const string MetaKeywordsIn = "@metaKeywords";

            public const string IngredientCategoryIdIn = "@ingredientCategoryId";

            public const string IngredientCategoryIdsIn = "@ingredientCategoryIds";

            public const string NotesIn = "@notes";

            public const string IngredientCategoryNameIn = "@ingredientCategoryName";

            public const string IngredientStatusIn = "@ingredientStatus";

            public const string CommentsIn = "@comments";

            public const string HairTypesIn = "@hairTypes";

            public const string HairRatingsIn = "@hairRatings";

            public const string IngredientRatingIdIn = "@ingredientRatingId";

            public const string PublicationIdIn = "@publicationId";

            public const string AuthorIdsIn = "@authorIds";

            public const string TitleHrefIn = "@titleHref";

            public const string YearPublishedIn = "@yearPublished";

            public const string PublisherIn = "@publisher";

            public const string CityIn = "@city";

            public const string StateProvinceIn = "@stateProvince";

            public const string CountryIn = "@country";

            public const string AuthorIdIn = "@authorId";

            public const string PrefixTypeIn = "@prefixType";

            public const string MiddleNameIn = "@middleName";

            public const string SuffixIn = "@suffix";

            public const string IngredientReferenceIdsIn = "@ingredientReferenceIds";

            public const string IngredientReferenceIdIn = "@ingredientReferenceId";

            public const string EditionIn = "@edition";

            public const string VolumeIn = "@volume";

            public const string SubtitleIn = "@subtitle";

            public const string SuffixTypeIn = "@suffixType";

            public const string PagesIn = "@pages";

            public const string ReferenceIdIn = "@referenceId";

            public const string IngredientLinkIdIn = "@ingredientLinkId";

            public const string TextIn = "@text";

            public const string HrefIn = "@href";

            public const string TooltipIn = "@tooltip";

            public const string LinkTypeIn = "@linkType";

            public const string HairRatingIdIn = "@hairRatingId";

            public const string RatingIn = "@rating";

            public const string HairTypeIdIn = "@hairTypeId";

            public const string TypeIn = "@type";

            public const string UrlEntryIdIn = "@urlEntryId";

            public const string RouteNameIn = "@routeName";

            public const string RouteUrlIn = "@routeUrl";

            public const string HandlerPathIn = "@handlerPath";

            public const string PriorityIn = "@priority";

            public const string ChangeFrequencyIn = "@changeFrequency";

            public const string ObjectTypeIn = "@objectType";

            public const string ObjectIdIn = "@objectId";

            public const string EmployeeIdIn = "@employeeId";

            public const string UsernameIn = "@username";

            public const string PasswordIn = "@password";

            public const string PasswordQuestionIn = "@passwordQuestion";

            public const string PasswordAnswerIn = "@passwordAnswer";

            public const string EmployeeGroupIdIn = "@employeeGroupId";

            public const string GroupNameIn = "@groupName";

            public const string EmployeeIdsIn = "@employeeIds";

            public const string OperationKindsIn = "@operationKinds";

            public const string LinksIn = "@links";

            public const string UriIn = "@uri";

            public const string UrlRouteIdIn = "@urlRouteId";

            public const string RouteTypeIn = "@routeType";

            public const string UrlEntryIdsIn = "@urlEntryIds";

            public const string IngredientIdOut = "@ingredientIdOut";

            public const string LocaleIdOut = "@localeIdOut";
        }

        public const string DeleteIngredientById = "DeleteIngredientById";

        public const string DeleteLocaleById = "DeleteLocaleById";

        public const string DeleteMessageTemplateKeyValuePairById = "DeleteMessageTemplateKeyValuePairById";

        public const string DeleteMessageTemplateById = "DeleteMessageTemplateById";

        public const string DeleteQuestionById = "DeleteQuestionById";

        public const string DeleteQuestionCategoryById = "DeleteQuestionCategoryById";

        public const string DeleteSettingById = "DeleteSettingById";

        public const string DeletePageById = "DeletePageById";

        public const string DeletePageContentById = "DeletePageContentById";

        public const string DeleteIngredientCategoryById = "DeleteIngredientCategoryById";

        public const string DeleteIngredientRatingById = "DeleteIngredientRatingById";

        public const string DeletePublicationById = "DeletePublicationById";

        public const string DeleteAuthorById = "DeleteAuthorById";

        public const string DeleteIngredientReferenceById = "DeleteIngredientReferenceById";

        public const string DeleteIngredientLinkById = "DeleteIngredientLinkById";

        public const string DeleteHairRatingById = "DeleteHairRatingById";

        public const string DeleteHairTypeById = "DeleteHairTypeById";

        public const string DeleteUrlEntryById = "DeleteUrlEntryById";

        public const string DeleteEmployeeById = "DeleteEmployeeById";

        public const string DeleteEmployeeGroupById = "DeleteEmployeeGroupById";

        public const string DeleteUrlRouteById = "DeleteUrlRouteById";

        public const string UpdateIngredient = "UpdateIngredient";

        public const string UpdateLocale = "UpdateLocale";

        public const string UpdateQuestion = "UpdateQuestion";

        public const string UpdateQuestionCategory = "UpdateQuestionCategory";

        public const string UpdateSetting = "UpdateSetting";

        public const string UpdateMessageCampaign = "UpdateMessageCampaign";

        public const string UpdatePage = "UpdatePage";

        public const string UpdatePageContent = "UpdatePageContent";

        public const string UpdateIngredientCategory = "UpdateIngredientCategory";

        public const string UpdatePublication = "UpdatePublication";

        public const string UpdateAuthor = "UpdateAuthor";

        public const string UpdateReference = "UpdateReference";

        public const string UpdateIngredientLink = "UpdateIngredientLink";

        public const string UpdateHairRating = "UpdateHairRating";

        public const string UpdateHairType = "UpdateHairType";

        public const string UpdateUrlEntry = "UpdateUrlEntry";

        public const string UpdateEmployee = "UpdateEmployee";

        public const string UpdateEmployeeGroup = "UpdateEmployeeGroup";

        public const string UpdateUrlRoute = "UpdateUrlRoute";

        public const string GetIngredientById = "GetIngredientById";

        public const string GetIngredients = "GetIngredients";

        public const string GetIngredientsByCategory = "GetIngredientsByCategory";

        public const string GetIngredientsByName = "GetIngredientsByName";

        public const string GetLocaleById = "GetLocaleById";

        public const string GetLocales = "GetLocales";

        public const string GetQuestionById = "GetQuestionById";

        public const string GetQuestionByQuestionAndAnswer = "GetQuestionByQuestionAndAnswer";

        public const string GetQuestionsByCategory = "GetQuestionsByCategory";

        public const string GetAllQuestions = "GetAllQuestions";

        public const string GetQuestionCategories = "GetQuestionCategories";

        public const string GetQuestionCategoriesByCategoryIds = "GetQuestionCategoriesByCategoryIds";

        public const string GetQuestionsByCriteria = "GetQuestionsByCriteria";

        public const string GetSettingById = "GetSettingById";

        public const string GetSettingByName = "GetSettingByName";

        public const string GetAllSettings = "GetAllSettings";

        public const string GetPageById = "GetPageById";

        public const string GetPageByName = "GetPageByName";

        public const string GetPageContentByPageId = "GetPageContentByPageId";

        public const string GetPageContentById = "GetPageContentById";

        public const string GetAllPages = "GetAllPages";

        public const string GetIngredientsByCategoryId = "GetIngredientsByCategoryId";

        public const string GetAllIngredientCategories = "GetAllIngredientCategories";

        public const string GetIngredientCategoryById = "GetIngredientCategoryById";

        public const string GetIngredientCategoryByName = "GetIngredientCategoryByName";

        public const string GetAllPublications = "GetAllPublications";

        public const string GetPublicationById = "GetPublicationById";

        public const string GetAllAuthors = "GetAllAuthors";

        public const string GetAuthorById = "GetAuthorById";

        public const string GetAllIngredientReferences = "GetAllIngredientReferences";

        public const string GetIngredientReferenceById = "GetIngredientReferenceById";

        public const string GetAuthorsByPublicationId = "GetAuthorsByPublicationId";

        public const string GetIngredientReferencesByIngredientId = "GetIngredientReferencesByIngredientId";

        public const string GetAllIngredientLinks = "GetAllIngredientLinks";

        public const string GetIngredientLinkById = "GetIngredientLinkById";

        public const string GetIngredientLinksByIngredientId = "GetIngredientLinksByIngredientId";

        public const string GetAllHairRatings = "GetAllHairRatings";

        public const string GetHairRatingById = "GetHairRatingById";

        public const string GetAllHairTypes = "GetAllHairTypes";

        public const string GetHairTypeById = "GetHairTypeById";

        public const string GetAllUrlEntries = "GetAllUrlEntries";

        public const string GetUrlEntryById = "GetUrlEntryById";

        public const string GetUrlEntryByKey = "GetUrlEntryByKey";

        public const string GetUrlEntryByObjectTypeAndObjectId = "GetUrlEntryByObjectTypeAndObjectId";

        public const string GetAllEmployees = "GetAllEmployees";

        public const string GetEmployeeById = "GetEmployeeById";

        public const string GetEmployeeByEmailAddress = "GetEmployeeByEmailAddress";

        public const string GetEmployeesByName = "GetEmployeesByName";

        public const string GetAllEmployeeGroups = "GetAllEmployeeGroups";

        public const string GetEmployeeGroupById = "GetEmployeeGroupById";

        public const string GetAllUrlRoutes = "GetAllUrlRoutes";

        public const string GetUrlRouteById = "GetUrlRouteById";

        public const string GetUrlEntriesByUrlRouteId = "GetUrlEntriesByUrlRouteId";

        public const string GetUrlRouteByRouteType = "GetUrlRouteByRouteType";

        public const string ConfirmSubscription = "ConfirmSubscription";

        public const string SetPageContentActive = "SetPageContentActive";
    }
}
