#pragma warning disable CA1707 // Remove underscores from test names - standard test naming convention
#pragma warning disable CA2234 // String overload is valid for our use case

using NUnit.Framework;
using System;
using Tuvi.App.ViewModels.Helpers;

namespace Tuvi.App.ViewModels.Tests
{
    [TestFixture]
    public class MailtoUriParserTests
    {
        [Test]
        public void Parse_SimpleMailtoWithRecipient_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:user@example.com";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.EqualTo("user@example.com"));
            Assert.That(result.Cc, Is.Empty);
            Assert.That(result.Bcc, Is.Empty);
            Assert.That(result.Subject, Is.Empty);
            Assert.That(result.Body, Is.Empty);
        }

        [Test]
        public void Parse_MailtoWithSubjectAndBody_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:user@example.com?subject=Hello&body=Test%20message";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.EqualTo("user@example.com"));
            Assert.That(result.Subject, Is.EqualTo("Hello"));
            Assert.That(result.Body, Is.EqualTo("Test message"));
        }

        [Test]
        public void Parse_MailtoWithCcAndBcc_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:user@example.com?cc=cc@example.com&bcc=bcc@example.com";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.EqualTo("user@example.com"));
            Assert.That(result.Cc, Is.EqualTo("cc@example.com"));
            Assert.That(result.Bcc, Is.EqualTo("bcc@example.com"));
        }

        [Test]
        public void Parse_MailtoWithMultipleRecipients_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:user1@example.com?to=user2@example.com&cc=cc1@example.com,cc2@example.com";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.EqualTo("user1@example.com, user2@example.com"));
            Assert.That(result.Cc, Is.EqualTo("cc1@example.com,cc2@example.com"));
        }

        [Test]
        public void Parse_MailtoWithEncodedCharacters_DecodesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:user@example.com?subject=Test%20Subject%20%26%20More&body=Line1%0ALine2";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.Subject, Is.EqualTo("Test Subject & More"));
            Assert.That(result.Body, Is.EqualTo("Line1\nLine2"));
        }

        [Test]
        public void Parse_MailtoWithoutRecipient_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:?subject=No%20Recipient&body=Body%20text";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.Empty);
            Assert.That(result.Subject, Is.EqualTo("No Recipient"));
            Assert.That(result.Body, Is.EqualTo("Body text"));
        }

        [Test]
        public void Parse_MailtoWithAllFields_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:primary@example.com?to=secondary@example.com&cc=cc@example.com&bcc=bcc@example.com&subject=Complete%20Test&body=Full%20message%20body";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.EqualTo("primary@example.com, secondary@example.com"));
            Assert.That(result.Cc, Is.EqualTo("cc@example.com"));
            Assert.That(result.Bcc, Is.EqualTo("bcc@example.com"));
            Assert.That(result.Subject, Is.EqualTo("Complete Test"));
            Assert.That(result.Body, Is.EqualTo("Full message body"));
        }

        [Test]
        public void Parse_WithNullUri_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => MailtoUriParser.Parse((Uri)null!));
        }

        [Test]
        public void Parse_WithNullString_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => MailtoUriParser.Parse((string)null!));
        }

        [Test]
        public void Parse_WithInvalidScheme_ThrowsArgumentException()
        {
            // Arrange
            var httpUri = new Uri("http://example.com");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => MailtoUriParser.Parse(httpUri));
        }

        [Test]
        public void Parse_CaseInsensitiveScheme_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "MAILTO:user@example.com?SUBJECT=Test";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.EqualTo("user@example.com"));
            Assert.That(result.Subject, Is.EqualTo("Test"));
        }

        [Test]
        public void Parse_CaseInsensitiveParameters_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:user@example.com?SUBJECT=Test&CC=cc@example.com&BCC=bcc@example.com&BODY=Message";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.Subject, Is.EqualTo("Test"));
            Assert.That(result.Cc, Is.EqualTo("cc@example.com"));
            Assert.That(result.Bcc, Is.EqualTo("bcc@example.com"));
            Assert.That(result.Body, Is.EqualTo("Message"));
        }

        [Test]
        public void Parse_EmptyMailto_ParsesCorrectly()
        {
            // Arrange
            var mailtoUri = "mailto:";

            // Act
            var result = MailtoUriParser.Parse(mailtoUri);

            // Assert
            Assert.That(result.To, Is.Empty);
            Assert.That(result.Cc, Is.Empty);
            Assert.That(result.Bcc, Is.Empty);
            Assert.That(result.Subject, Is.Empty);
            Assert.That(result.Body, Is.Empty);
        }
    }
}
