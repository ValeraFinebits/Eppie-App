// ---------------------------------------------------------------------------- //
//                                                                              //
//   Copyright 2026 Eppie (https://eppie.io)                                    //
//                                                                              //
//   Licensed under the Apache License, Version 2.0 (the "License"),            //
//   you may not use this file except in compliance with the License.           //
//   You may obtain a copy of the License at                                    //
//                                                                              //
//       http://www.apache.org/licenses/LICENSE-2.0                             //
//                                                                              //
//   Unless required by applicable law or agreed to in writing, software        //
//   distributed under the License is distributed on an "AS IS" BASIS,          //
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.   //
//   See the License for the specific language governing permissions and        //
//   limitations under the License.                                             //
//                                                                              //
// ---------------------------------------------------------------------------- //

using Eppie.App.ViewModels.Tests.TestDoubles;
using NUnit.Framework;
using Tuvi.Core.Entities;

namespace Eppie.App.ViewModels.Tests
{
    public class ComposeMessageDraftTests
    {
        [Test]
        public void NavigatingAwaySavesCurrentComposerContent()
        {
            var core = new FakeTuviMail();
            var account = new Account { Email = new EmailAddress("sender@example.com") };
            var vm = CreateViewModel(core, account);
            var recipient = new ContactItem(new EmailAddress("recipient@example.com"));
            var attachment = new RemovableAttachment { FileName = "test.txt", Data = new byte[] { 1, 2, 3 } };

            vm.To.Add(recipient);
            vm.Subject = "Distinctive subject";
            vm.TextBody = "Current body";
            vm.Attachments.Add(attachment);

            vm.OnNavigatedFrom();

            var draft = core.CreatedDraftMessages.Single();
            Assert.Multiple(() =>
            {
                Assert.That(draft.To, Has.Exactly(1).Property(nameof(EmailAddress.Address)).EqualTo("recipient@example.com"));
                Assert.That(draft.Subject, Is.EqualTo("Distinctive subject"));
                Assert.That(draft.TextBody, Is.EqualTo("Current body"));
                Assert.That(draft.Attachments, Has.Exactly(1).Property(nameof(Attachment.FileName)).EqualTo("test.txt"));
            });
        }

        [Test]
        public void NavigatingAwayUpdatesExistingDraft()
        {
            var core = new FakeTuviMail();
            var account = new Account { Email = new EmailAddress("sender@example.com") };
            var vm = CreateViewModel(core, account);
            var existingMessage = new Message
            {
                Id = 7,
                Folder = new Folder { Account = account }
            };
            vm.MessageInfo = new MessageInfo(existingMessage);
            vm.Subject = "Updated subject";
            vm.TextBody = "Updated body";

            vm.OnNavigatedFrom();

            Assert.Multiple(() =>
            {
                Assert.That(core.UpdatedDraftMessages, Has.Count.EqualTo(1));
                Assert.That(core.UpdatedDraftMessages[0].Id, Is.EqualTo(7));
                Assert.That(core.UpdatedDraftMessages[0].Subject, Is.EqualTo("Updated subject"));
                Assert.That(core.UpdatedDraftMessages[0].TextBody, Is.EqualTo("Updated body"));
            });
        }

        private static ComposeMessagePageViewModel CreateViewModel(FakeTuviMail core, Account account)
        {
            var vm = new ComposeMessagePageViewModel();
            vm.SetCoreProvider(() => core);
            vm.SenderAddresses.Add(new AddressItem(account));
            vm.SenderAddressIndex = 0;
            return vm;
        }
    }
}
