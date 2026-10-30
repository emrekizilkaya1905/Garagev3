# Garage 3.0 — Yapılacaklar

Tamamlanan maddeleri `- [x]` yapacağız. Tamamlanan ve doğrulanan bölümler işaretlenir; ilerideki mimari dönüşümlerde davranışlar tekrar kontrol edilir. Kod, yorumlar ve konsol mesajları İngilizce; bu takip belgesi Türkçe olacak.

## 1. Mevcut durum ve yeni hedef

Proje Garagev2'den kopyalandı ve namespace'ler Garagev3 olarak güncellendi. Mevcut Garage private List<Vehicle> kullanıyor. Konsol arayüzü, beş araç türü ve arama kodları mevcut. Yeni ödevin temel ekleri generics, interface'ler, GarageHandler ve ayrı unit test projesi.

- [x] Mevcut park etme, çıkarma, listeleme ve arama davranışlarını başlangıç kontrolüyle doğrula; sonraki mimari dönüşümlerde de koru.
- [x] Konsol başlığını Garage 3.0 olarak güncelle.
- [x] README'deki proje adı, çalıştırma komutları ve mimari açıklamasını Garagev3'e uyarla.

## 2. Araç modeli — zorunlu

- [x] Vehicle ortak özellikleri tutsun: RegistrationNumber, Color, NumberOfWheels.
- [x] Vehicle abstract olarak kalsın; doğrudan genel araç oluşturulmasın.
- [x] Airplane: NumberOfEngines gibi en az bir özel özellik.
- [x] Motorcycle: CylinderVolume gibi en az bir özel özellik.
- [x] Car: FuelType gibi en az bir özel özellik; mevcut enum'u koru.
- [x] Bus: NumberOfSeats gibi en az bir özel özellik.
- [x] Boat: Length gibi en az bir özel özellik.
- [x] Ortak ve özel özellikler constructor üzerinden doğrulansın.
- [x] Araçların ortak ve özel özellikleri listelemede gösterilsin.
- [x] Plaka, park edildikten sonra değiştirilip benzersizlik kuralı aşılamasın.
- [x] Önceki sürümde eklenen ABC123 / ABC12A format kontrolünü koru (bu metinde ayrıca belirtilmiyor).

## 3. Interface'ler ve katmanlar — yeni ve zorunlu

Amaç: UI ile Garage arasında doğrudan bağlantı olmasın. Kullanıcıyla tüm etkileşim UI katmanında gerçekleşsin.

Önerilen akış:

```text
Program → IUI / ConsoleUI → IHandler / GarageHandler → Garage<Vehicle>
                          → IVehicle / Vehicle ve alt sınıflar
```

Interface adları örnektir; önemli olan uygulamanın uygun noktalarda somut sınıflar yerine sözleşmelere bağımlı olmasıdır.

- [ ] IVehicle oluştur; bütün araçların ortak property'lerini tanımla.
- [ ] Vehicle sınıfının IVehicle uygulamasını sağla.
- [ ] IUI oluştur; uygulamanın arayüz giriş metodunu, örneğin Run(), tanımla.
- [ ] ConsoleUI sınıfının IUI uygulamasını sağla.
- [ ] IHandler oluştur; UI'nin ihtiyaç duyduğu garaj işlemlerini tanımla.
- [ ] GarageHandler oluştur ve IHandler uygulat.
- [ ] GarageHandler içine Console.ReadLine / Console.WriteLine koyma; veri ve işlem sonucu döndür.
- [ ] UI içindeki doğrudan new Garage(...), Garage.Park, Garage.Remove, Garage.Search çağrılarını handler üzerinden geçir.
- [ ] GarageMenuActions sınıfını IHandler kullanacak şekilde düzenle veya görevlerini uygun sınıflara taşı.
- [ ] UI'nin kapasite ve doluluk bilgisini de handler üzerinden almasını sağla.
- [ ] Program.cs yalnızca nesneleri bağlayıp uygulamayı başlatsın; başlangıçta somut sınıfları oluşturmak normaldir.
- [ ] Gereksiz interface ve katman eklemeden sözleşmeleri ihtiyaçlara göre küçük tut.

### Handler için önerilen sorumluluklar

- [ ] Kullanıcı kapasitesiyle garaj oluşturma.
- [ ] Garaj oluşturulmadan işlem yapılmasını yönetme.
- [ ] Araç ekleme ve çıkarma işlemlerini Garage'a iletme.
- [ ] Liste, tür sayıları, plaka araması ve birleşik arama sonuçlarını UI'ye sunma.
- [ ] Örnek araçları oluşturup normal park kurallarıyla ekleme.
- [ ] Başarı/başarısızlık ve nedenini OperationResult gibi bir sonuçla döndürme.

## 4. Generic Garage<T> — yeni ve zorunlu

- [ ] Garage sınıfını Garage<T> yap.
- [ ] Generic tipe araç kısıtı koy. Öneri: `where T : Vehicle`; IVehicle tabanlı tasarım seçilirse `where T : class, IVehicle` da değerlendirilebilir. Bir tutarlı yaklaşım seç.
- [ ] İç koleksiyonu private List<T> olarak tut. Bu ödev metni iç depolama için array zorunluluğu getirmiyor.
- [ ] Constructor kapasite alsın: Garage(int capacity).
- [ ] Sıfır ve negatif kapasite reddedilsin.
- [ ] Capacity, List.Capacity'den ayrı bir park sınırı olsun.
- [ ] Count doğrudan koleksiyondaki araç sayısını yansıtsın.
- [ ] Park metodu T kabul etsin.
- [ ] Liste ve arama dönüşlerini generic tipe uygun düzenle.
- [ ] İç koleksiyonun dışarıdan değiştirilmesini engelle; kopya veya uygun salt okunur görünüm döndür.
- [ ] Kapasite ve benzersizlik kurallarını Garage içinde uygula; yalnızca UI kontrolüne güvenme.

### IEnumerable<T> ve foreach

- [ ] Garage<T>, IEnumerable<T> uygulasın.
- [ ] Public IEnumerator<T> GetEnumerator() metodunu uygula.
- [ ] Generic olmayan IEnumerable.GetEnumerator() metodunu da uygula; generic enumerator'a yönlendirebilir.
- [ ] `foreach (Vehicle vehicle in garage)` çalışsın.
- [ ] İterasyonda yalnızca park edilmiş araçlar yer alsın.
- [ ] Boş garaj üzerinde foreach hata vermesin.

## 5. İşlevler — zorunlu

- [ ] Bütün park edilmiş araçları özellikleriyle listele.
- [ ] Araç türlerini ve her türün sayısını listele.
- [ ] Araç park et.
- [ ] Araç çıkar; alan yeniden kullanılabilsin.
- [ ] Dolu garaja araç eklenmesini reddet ve nedenini bildir.
- [ ] Aynı plakalı aracın eklenmesini reddet; mevcut araç değişmesin.
- [ ] ABC123 / Abc123 / AbC123 aynı plaka olarak değerlendirilsin.
- [ ] Plakayla belirli bir aracı bul.
- [ ] Bulunmayan araç için anlaşılır sonuç döndür.
- [ ] Başlangıçta isteğe bağlı olarak belirli sayıda örnek araç ekleme imkânı sun. Otomatik ekleme veya beş araç ekleme zorunlu değil.
- [ ] Örnek araçlarda kapasite ve plaka kontrollerini atlama.
- [ ] Başarı ve hata mesajlarını UI'de göster.

## 6. Arama ve filtreleme — zorunlu

- [ ] VehicleSearchCriteria gibi bir ölçüt nesnesi kullan.
- [ ] Plaka filtresi.
- [ ] Renk filtresi.
- [ ] Tekerlek sayısı filtresi.
- [ ] Araç türü filtresi.
- [ ] Vehicle'a eklenen başka ortak özellikler varsa onların filtrelerini de sağla.
- [ ] Ortak özelliklerin bütün kombinasyonlarıyla arama yapılabilsin.
- [ ] Birden fazla kriter AND / VE mantığıyla uygulansın; sonuçlar birleştirilmesin.
- [ ] Boş bırakılan alanlar filtre uygulamasın.
- [ ] Tekerlek sayısında null ile 0 farklı anlam taşısın.
- [ ] Hiç filtre girilmezse bütün araçlar gösterilsin.
- [ ] Tek kriter ve çoklu kriter araması kullanıcıya anlaşılır sunulsun.
- [ ] Sonuç yoksa bilgi verilsin.

## 7. Konsol ve sağlam giriş yönetimi — zorunlu

- [ ] Ana menü ve gereken alt menüler bulunsun.
- [ ] Bütün zorunlu işlevlere UI üzerinden erişilsin.
- [ ] Garaj kapasitesini kullanıcı belirlesin.
- [ ] Kullanıcı uygulamadan çıkabilsin.
- [ ] Sayı girişlerinde TryParse ve aralık kontrolü kullan.
- [ ] Boş giriş, harf, negatif sayı ve int sınırını aşan değerleri yönet.
- [ ] Geçersiz menü seçimlerinde tekrar seçim iste.
- [ ] Boş garajdan araç çıkarma uygulamayı çökertmesin.
- [ ] Listeden araç seçerken index sınırlarını kontrol et.
- [ ] İşlem iptali çalışsın.
- [ ] Console.ReadLine null dönerse sonsuz döngüye girmeden çık.
- [ ] Constructor doğrulamalarıyla UI doğrulamaları aynı kuralları kullansın.
- [ ] Tüm kullanıcı etkileşimi UI'de kalsın; Garage ve handler konsola bağımlı olmasın.

## 8. Ayrı unit test projesi — zorunlu

- [ ] Çözümün yanında Garagev3.Tests adlı ayrı test projesi oluştur.
- [ ] Uygulama projesine ProjectReference ekle.
- [ ] Bir test framework'ü seç ve gerekli test paketlerini ekle.
- [ ] Test projesini solution'a ekle.
- [ ] Bu ödev kapsamında Garage'ın public metotlarına odaklan; bütün UI'yi test etmek ek görevdir.
- [ ] Testleri Arrange → Act → Assert biçiminde düzenle.
- [ ] Adları `MethodName_StateUnderTest_ExpectedBehavior` şeklinde açıklayıcı yaz.
- [ ] İstersen yeni işlevlerde önce başarısız testi yaz, sonra kodu uygula; TDD öneridir, zorunlu değildir.

### Kapasite ve park etme testleri

- [ ] Constructor_PositiveCapacity_CreatesEmptyGarage
- [ ] Constructor_ZeroOrNegativeCapacity_ThrowsArgumentOutOfRangeException
- [ ] Park_AvailableSpace_AddsVehicleAndIncreasesCount
- [ ] Park_FullGarage_ReturnsFailureWithoutChangingVehicles
- [ ] Park_DuplicateRegistration_ReturnsFailure
- [ ] Park_DuplicateRegistrationWithDifferentCase_ReturnsFailure
- [ ] Park_NullVehicle_ThrowsArgumentNullException

### Çıkarma ve plaka arama testleri

- [ ] Remove_ExistingVehicle_RemovesVehicleAndDecreasesCount
- [ ] Remove_UnknownRegistration_ReturnsFailureWithoutChangingCount
- [ ] Remove_EmptyGarage_ReturnsFailure
- [ ] Remove_DifferentRegistrationCase_RemovesVehicle
- [ ] Park_AfterRemoval_ReusesAvailableCapacity
- [ ] FindByRegistrationNumber_ExistingRegistration_ReturnsCorrectVehicle
- [ ] FindByRegistrationNumber_DifferentCase_ReturnsCorrectVehicle
- [ ] FindByRegistrationNumber_UnknownOrBlankRegistration_ReturnsNull

### Liste, tür sayıları ve arama testleri

- [ ] GetAllVehicles_EmptyGarage_ReturnsEmptyCollection
- [ ] GetAllVehicles_ParkedVehicles_ReturnsAllVehicles
- [ ] GetAllVehicles_ModifiedReturnedCollection_DoesNotChangeGarage
- [ ] GetVehicleTypeCounts_MixedVehicles_ReturnsCorrectCounts
- [ ] GetVehicleTypeCounts_AfterRemoval_UpdatesCounts
- [ ] Search_SingleCriterion_ReturnsOnlyMatchingVehicles
- [ ] Search_MultipleCriteria_ReturnsOnlyVehiclesMatchingEveryCriterion
- [ ] Search_ColorWithDifferentCase_ReturnsMatchingVehicles
- [ ] Search_ZeroWheels_ReturnsZeroWheeledVehicles
- [ ] Search_VehicleType_ReturnsOnlyRequestedType
- [ ] Search_NoCriteria_ReturnsAllVehicles
- [ ] Search_NoMatches_ReturnsEmptyCollection
- [ ] Search_NullCriteria_ThrowsArgumentNullException
- [ ] Search_NegativeWheels_ThrowsArgumentOutOfRangeException
- [ ] Search_InvalidVehicleType_ThrowsArgumentException
- [ ] Search_RegistrationColorWheelsCombinations_ReturnsExpectedMatches (veri odaklı testlerle kombinasyonları kapsa)

### Generic yapı ve iterasyon testleri

- [ ] GetEnumerator_ParkedVehicles_EnumeratesAllVehicles
- [ ] GetEnumerator_EmptyGarage_EnumeratesNoVehicles
- [ ] NonGenericGetEnumerator_ParkedVehicles_EnumeratesAllVehicles
- [ ] Garage<Car> örneğinin Car nesneleriyle çalıştığını doğrula.
- [ ] Garage<Vehicle> örneğinin farklı alt sınıfları birlikte tutabildiğini doğrula.
- [ ] Araç olmayan tiplerin constraint yüzünden kullanılamadığını derleme düzeyinde kontrol et; geçersiz kodu normal test projesine ekleme.

> Test adları öneridir. Seçilen API'nin hata davranışı değişirse test adlarını ve beklentilerini tutarlı güncelle.

## 9. Önerilen geliştirme sırası

- [ ] 1. Generic Garage<T>, constraint ve IEnumerable<T> dönüşümünü yap.
- [ ] 2. Garage'ın temel public metot testlerini yaz ve çalıştır.
- [ ] 3. IVehicle, IHandler ve IUI sözleşmelerini oluştur.
- [ ] 4. GarageHandler'ı ekleyip garaj oluşturma ve işlev çağrılarını taşı.
- [ ] 5. ConsoleUI ve GarageMenuActions'ı handler'a bağla; doğrudan Garage erişimini kaldır.
- [ ] 6. Örnek veri akışını handler üzerinden düzenle.
- [ ] 7. Arama kombinasyonlarını ve kalan Garage testlerini tamamla.
- [ ] 8. Menüden bütün temel akışları elle doğrula.
- [ ] 9. README'yi güncelle ve son derleme/test kontrolünü yap.

## 10. İsteğe bağlı ekler — zorunlu değil

- [ ] Alt sınıflara özel özelliklerle arama.
- [ ] Birden fazla garaj ve aktif garaj seçimi.
- [ ] Hangar, otomobil garajı, motosiklet garajı gibi tip kısıtlı garajlar.
- [ ] Aktif garaj adını gösterme.
- [ ] ParkingSpace sınıfıyla park yeri modelleme.
- [ ] Dosyaya kaydetme ve dosyadan yükleme.
- [ ] Farklı alan tüketimi: otomobil 1, tekne 2, uçak 3, motosiklet 1/3 yer.
- [ ] Kesirli alan hesabında üçte birlik tamsayı birimleri kullanmayı değerlendir: otomobil 3, tekne 6, uçak 9, motosiklet 1 birim.
- [ ] Park menüsünde yalnızca sığabilecek türleri gösterme.
- [ ] Kapasiteyi yapılandırmadan okuma.
- [ ] Handler ve UI için ek testler.

## 11. Teslim öncesi kontrol

- [ ] Garage<T>, constraint ve iki GetEnumerator uygulaması mevcut.
- [ ] UI ile Garage arasında doğrudan bağlantı yok.
- [ ] Interface'ler gerçekten kullanılıyor; yalnızca tanımlanmış durumda değiller.
- [ ] Beş araç alt sınıfı ve her birinin özel özelliği mevcut.
- [ ] Kapasite, benzersizlik ve birleşik filtreleme doğru çalışıyor.
- [ ] Başlangıç doldurma imkânı mevcut.
- [ ] Hatalı kullanıcı girişleri uygulamayı çökertmiyor.
- [ ] Ayrı test projesindeki Garage testleri geçiyor.
- [ ] Solution derleniyor ve test komutu başarılı.
- [ ] README kurulum, çalıştırma, test ve sınıf sorumluluklarını açıklıyor.
- [ ] Gereksiz veya eski sürüme ait kod ve açıklamalar temizlendi.

