# Wapplo
Extend your Web-Applications with desktop services

Wapplo is currently in a concept phase.

With this application it will be possible to extend web applications, meaning web sites (HTML) in the browser, with services which normally are only available to desktop applications. This was especially written for usage in companies, where currently all (or most) applications are web based, but have a need which is very hard to cover.

All services will be available via technologies which are supported in all major browser, and do not rely on old technologies like OCX/ActiveX 

Some possible services (not limited to) which can be offered:
1. A real time communication channel between two or more web applications. For instance to share the current customer, and some status, on which the user is working. To have a secure channel, both applications can consider signing or encrypting the local application *should* not be responsible for that.
2. For applications which offer work routing, it might be handy to know if the user is currently away from the keyboard, has his desktop locked.
3. An real interface to Outlook, have you web application open an new email with all the information (like to, cc, bcc, subject etc) and even know when the email was sent.
4. Access the user's Calendar.
5. Open some other application.
6. Have a native UI, inform the user via "popups" or Toasts.
7. Enhanced security, for instance via a USB dongle or the likes.
8. Access your ticketing system, to report issues from your application
9. Share information between users

Currently this application will only work on Windows, but as .NET Core progresses and some of the needed technology is ported to this, it will also work on Linux or OSX.
