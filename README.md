# Coursera.NET

A Microsoft .NET Library for interacting with Coursera.

Usage, at this point, is relatively straight forward. Below are some examples that show the basic functionality of Coursera.NET.

## 1. Logging In

Logging in is achieved through the creation of a Coursera session. Depending on your platform, you will have to choose the appropriate `RequestBuilder`. This requirement should be removed with the next version once the PCL version of `WebRequest` is used.

```C#
// Creates a Desktop Session
var session = Session.Login(username, password, new DesktopRequestBuilder());
```

## 2. Getting Course List

Once you have a session, the next thing you'll need to worry about is getting a list of the user's courses.

```C#
var courseService = new GetCoursesService(session);
var courses = await courseService.ExecuteAsync();
```

## 3. Getting Course Options

Now that you have your courses, it's time to chose one and get the available options for that course.

```C#
var courseOptionService = new GetCourseOptionsService(selectedCourse, session);
var courseOptions = await courseOptionService.ExecuteAsync();
```

## 4. Navigating Options

This is where things get a little complicated. Each course is going to have different options. We need something that can handle arbitrary requests. Luckily, there's a request for that! We just need to know the expected return type and the appropriate formatter to get there. Let's assume we're trying to get Lecture Groups (the process will be the same for all options).

```C#
var lectureGroupsService = 
    new ArbitraryRequest<IEnumerable<LectureGroup>>(
        selectedCourse,
        selectedOption.Url,
        new LectureGroupFormatter(),
        session);

var lectureGroups = await lectureGroupsService.ExecuteAsync();
```

## 5. Getting Lecture Streaming URL

We've pretty much covered everything you need at this point. The last little bit concerns Coursera's streaming video. A `Lecture` will have both an `IframeUrl` and a `ProtectedVideoUrl`. The `ProtectedVideoUrl` is perfect for downloading and watching later. You should use the `IframeUrl` for streaming. Before you can do that, it needs to go through an `ArbitraryService` as well:

```C#
var streamingVideoUrlService =
    new ArbitraryRequest<string>(
        selectedCourse,
        lecture.Lecture.IframeUrl,
        new StreamingVideoFormatter(),
        session);

var streamingVideoUrl = await streamingVideoUrlService.ExecuteAsync();
```

## 6. Getting Lecture Download URL

As you may have guessed by the name, downloading a video isn't quite straightforward either. First, we need to unprotect the name and get the real URL.

```C#
var downloadUrl = 
    await session.UnprotectedProtectedAsssetUrl(lecture.Lecture.ProtectedVideoUrl, 
        HttpVerb.Get);
```