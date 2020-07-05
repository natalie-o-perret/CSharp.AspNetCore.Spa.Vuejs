using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace CSharp.AspNetCore.Spa.Vuejs.DevExtreme
{
    public static class DataSourceLoadOptionsParser
    {
        private static string GetFieldStringValue(ModelBindingContext modelBindingContext, string fieldName)
            => modelBindingContext.ValueProvider.GetValue(fieldName).FirstOrDefault();

        private static Result<T, FieldError> ValidatePrimitiveString<T>(
            ModelBindingContext modelBindingContext,
            string fieldName,
            Func<string, (bool IsValid, T Value)> tryParse)
            where T : struct
        {
            var fieldStringValue = GetFieldStringValue(modelBindingContext, fieldName);
            if (string.IsNullOrEmpty(fieldStringValue))
            {
                return Result.Success<T, FieldError>(default);
            }

            var (isValid, value) = tryParse(fieldStringValue);

            return (isValid, value) switch
            {
                (true, _) =>
                Result.Success<T, FieldError>(value),
                (false, _) =>
                Result.Failure<T, FieldError>(new FieldError { Name = fieldName, Value = fieldStringValue })
            };
        }

        private static Result<int, FieldError> ValidateInt32String(
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidatePrimitiveString(modelBindingContext, fieldName, str =>
            {
                var isOk = Int32.TryParse(str, out var value);
                return (isOk, value);
            });

        private static Result<bool, FieldError> ValidateBoolString(
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidatePrimitiveString(modelBindingContext, fieldName, str =>
            {
                var isOk = Boolean.TryParse(str, out var value);
                return (isOk, value);
            });

        private static Result<T,FieldError> ValidateJsonString<T>(
            ModelBindingContext modelBindingContext,
            string fieldName,
            Func<string, T> deserialize)
        {
            var fieldStringValue = GetFieldStringValue(modelBindingContext, fieldName);
            if (string.IsNullOrEmpty(fieldStringValue))
            {
                return Result.Success<T, FieldError>(default!);
            }

            try
            {
                return Result.Success<T, FieldError>(deserialize(fieldStringValue));
            }
            catch
            {
                return Result.Failure<T, FieldError>(new FieldError { Name = fieldName, Value = fieldStringValue } );
            }
        }

        private static Result<SortingInfo[], FieldError> ValidateSortingInfoString (
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidateJsonString(modelBindingContext, fieldName, JsonConvert.DeserializeObject<SortingInfo[]>);

        private static Result<GroupingInfo[],FieldError> ValidateGroupingInfoString (
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidateJsonString(modelBindingContext, fieldName, JsonConvert.DeserializeObject<GroupingInfo[]>);

        private static Result<IList, FieldError> ValidateFilterString(
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidateJsonString(modelBindingContext, fieldName, str =>
                JsonConvert.DeserializeObject<IList>(
                    str,
                    new JsonSerializerSettings { DateParseHandling = DateParseHandling.None})!);

        private static Result<SummaryInfo[], FieldError> ValidateSummaryInfoString(
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidateJsonString(modelBindingContext, fieldName, JsonConvert.DeserializeObject<SummaryInfo[]>);

        private static Result<string[], FieldError> ValidateStringArrayString(
            ModelBindingContext modelBindingContext,
            string fieldName) =>
            ValidateJsonString(modelBindingContext, fieldName, JsonConvert.DeserializeObject<string[]>);

        public static Result<DataSourceLoadOptions, IList<FieldError>> Parse(ModelBindingContext modelBindingContext)
        {
            var requireTotalCount = ValidateBoolString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.RequireTotalCountKey);
            var requireGroupCount = ValidateBoolString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.RequireGroupCountKey);
            var isCountQuery = ValidateBoolString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.IsCountQueryKey);
            var skip = ValidateInt32String(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.SkipKey);
            var take = ValidateInt32String(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.TakeKey);
            var sort = ValidateSortingInfoString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.SortKey);
            var group = ValidateGroupingInfoString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.GroupKey);
            var filter = ValidateFilterString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.FilterKey);
            var totalSummary = ValidateSummaryInfoString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.TotalSummaryKey);
            var groupSummary = ValidateSummaryInfoString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.GroupSummaryKey);
            var select = ValidateStringArrayString(modelBindingContext, DataSourceLoadOptionsBaseFieldNames.SelectKey);

            var failures = new List<FieldError>();

            if (requireGroupCount.IsFailure)
            {
                failures.Add(requireGroupCount.Error);
            }
            if (requireTotalCount.IsFailure)
            {
                failures.Add(requireTotalCount.Error);
            }
            if (isCountQuery.IsFailure)
            {
                failures.Add(isCountQuery.Error);
            }
            if (skip.IsFailure)
            {
                failures.Add(skip.Error);
            }
            if (take.IsFailure)
            {
                failures.Add(take.Error);
            }
            if (sort.IsFailure)
            {
                failures.Add(sort.Error);
            }
            if (group.IsFailure)
            {
                failures.Add(group.Error);
            }
            if (filter.IsFailure)
            {
                failures.Add(filter.Error);
            }
            if (totalSummary.IsFailure)
            {
                failures.Add(totalSummary.Error);
            }
            if (groupSummary.IsFailure)
            {
                failures.Add(groupSummary.Error);
            }
            if (select.IsFailure)
            {
                failures.Add(select.Error);
            }

            if (failures.Any())
            {
                return Result.Failure<DataSourceLoadOptions, IList<FieldError>>(failures);
            }

            var loadOptions = new DataSourceLoadOptions
            {
                RequireTotalCount = requireTotalCount.Value,
                RequireGroupCount = requireGroupCount.Value,
                IsCountQuery = isCountQuery.Value,
                Skip = skip.Value,
                Take = take.Value,
                Sort = sort.Value,
                Group = group.Value,
                Filter = filter.Value,
                TotalSummary = totalSummary.Value,
                GroupSummary = groupSummary.Value,
                Select = select.Value
            };

            return Result.Success<DataSourceLoadOptions, IList<FieldError>>(loadOptions);
        }
    }
}