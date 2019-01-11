/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 80013
Source Host           : localhost:3306
Source Database       : vlxm

Target Server Type    : MYSQL
Target Server Version : 80013
File Encoding         : 65001

Date: 2019-01-07 18:13:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for article_info
-- ----------------------------
DROP TABLE IF EXISTS `article_info`;
CREATE TABLE `article_info` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `title` varchar(255) NOT NULL COMMENT '标题',
  `userId` bigint(20) DEFAULT NULL,
  `author` varchar(255) DEFAULT NULL COMMENT '作者',
  `articleType` int(11) NOT NULL COMMENT '文章类型',
  `category` varchar(255) NOT NULL COMMENT '类别',
  `description` varchar(255) DEFAULT NULL COMMENT '描述',
  `content` text NOT NULL COMMENT '内容',
  `faceImg` varchar(255) NOT NULL DEFAULT '0' COMMENT '封面图',
  `status` int(11) NOT NULL DEFAULT '0' COMMENT '状态',
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` datetime DEFAULT NULL,
  `publishTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '发布时间',
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章信息';

-- ----------------------------
-- Records of article_info
-- ----------------------------
INSERT INTO `article_info` VALUES ('29', 'doo', '2018', 'la na na ', '3', '11', '在使用reactjs库的时候，会遇到将一段html的字符串，然后要将它插入页面中以html的形式展现，然而直接插入的话页面显示的就是这段字符串，而不会进行转义，可以用一下方法插入，便可以html的形式展现：', '<pre><span style=\"font-size:14px\"><span style=\"color:#4f4f4f\"><span style=\"font-size:nanpx\"><span style=\"color:#006666\">&lt;divdangerouslySetInnerHTML=</span></span>{{__<span style=\"font-size:nanpx\">html</span>: <span style=\"font-size:nanpx\"><span style=\"color:#009900\">&quot;&lt;p&gt;内容呢&lt;/p&gt;&quot;</span></span>}}<span style=\"font-size:nanpx\"><span style=\"color:#006666\"> /&gt;</span></span></span></span><br/></pre><p>??</p><p></p><hr/><p></p><hr/><p></p><hr/><p></p><hr/><p></p><hr/><p></p><p>123123123</p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/d4733ff3-055e-4e54-b41c-bad2a2390313\"/></div><p>图片呢</p><div class=\"media-wrap image-wrap\"><img src=\"blob:http://localhost:4444/d4733ff3-055e-4e54-b41c-bad2a2390313\"/></div><p></p>', '/upload/image/2018-12-29/d49b76ae3d4f49b6ab0cea88923a1b99.jpg', '0', '2018-12-29 11:28:40', null, '2018-12-28 16:00:01', '1');
INSERT INTO `article_info` VALUES ('39', 'iis 405', '1', 'x', '1', '7', '解决方法', '<p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">&lt;system.webServer&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">  &lt;modules&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">    &lt;remove name=&quot;WebDAVModule&quot; /&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">  &lt;/modules&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">  &lt;handlers&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">    &lt;remove name=&quot;WebDAV&quot; /&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">  &lt;/handlers&gt; </span></span></span></span></p><p><span style=\"padding-left:0px;padding-right:0px\"><span style=\"color:#000000\"><span style=\"background-color:#ffffff\"><span style=\"font-size:14px\">&lt;/system.webServer&gt;</span></span></span></span></p>', '/upload/image/2019-01-02/ff32397fc7d94b4baa9676553fd1f402.jpg', '0', '2019-01-02 16:25:56', null, '2019-01-02 08:25:55', '1');
INSERT INTO `article_info` VALUES ('40', 'is 帮助文章地址', '1', 'monster', '3', '7', '{{domain_url}}/.well-known/openid-configuration', '', '/upload/image/2019-01-03/c604f0a854a84f5898013aa281729cad.jpg', '0', '2019-01-03 14:11:02', null, '2019-01-03 06:10:29', '1');
INSERT INTO `article_info` VALUES ('42', 'axios中文api', '1', 'x', '3', '7', 'https://www.kancloud.cn/yunye/axios/234845', '', '/upload/image/2019-01-04/9b9d35cc461e4e95b1b281d953933a6f.jpg', '0', '2019-01-04 14:06:29', null, '2019-01-04 06:06:28', '1');
INSERT INTO `article_info` VALUES ('45', 'new week is begin', '1', 'monster', '3', '3', null, '<p>?</p>', '/upload/image/2019-01-07/2122650b0b8742a9b4fd7427713d8bb3.jpg', '0', '2019-01-07 10:58:52', null, '2019-01-07 02:58:50', '1');
INSERT INTO `article_info` VALUES ('46', 'asp.net web api json返回首字母小写', '1', 'monster', '1', '6', '.net JsonConverter 在类上进行注解\nfreamwork 4.5 \n例如想在类上进行注解 使得类的所有属性以小写进行输出', '<pre><br/>      GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();</pre>', '/upload/image/2019-01-07/63faaa4dcd3b4d0fad39af0d1d5fa138.jpg', '0', '2019-01-07 11:57:12', null, '2019-01-07 03:56:49', '1');

-- ----------------------------
-- Table structure for article_opt_info
-- ----------------------------
DROP TABLE IF EXISTS `article_opt_info`;
CREATE TABLE `article_opt_info` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `actionKey` varchar(50) DEFAULT NULL COMMENT '操作key',
  `optionType` int(11) DEFAULT NULL COMMENT '操作类型',
  `relationKey` bigint(20) DEFAULT NULL COMMENT '关联id',
  `count` int(11) DEFAULT NULL COMMENT '操作数',
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '创建时间',
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  `actionUser` bigint(20) NOT NULL COMMENT '触发人',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章操作表';

-- ----------------------------
-- Records of article_opt_info
-- ----------------------------
INSERT INTO `article_opt_info` VALUES ('2', 'Like', '1', '1', '1', '2019-01-07 16:29:10', null, '1', '1');
INSERT INTO `article_opt_info` VALUES ('3', 'Like', '1', '2', '1', '2019-01-07 16:33:11', '2019-01-07 17:04:59', '1', '1');
INSERT INTO `article_opt_info` VALUES ('4', 'Like', '1', '46', '1', '2019-01-07 17:02:38', '2019-01-07 17:05:09', '0', '1');

-- ----------------------------
-- Table structure for article_tag
-- ----------------------------
DROP TABLE IF EXISTS `article_tag`;
CREATE TABLE `article_tag` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `tagName` varchar(50) DEFAULT NULL,
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章标签';

-- ----------------------------
-- Records of article_tag
-- ----------------------------
INSERT INTO `article_tag` VALUES ('1', '青春', '2018-12-27 15:07:10', null, '1');
INSERT INTO `article_tag` VALUES ('2', '校园', '2018-12-27 15:07:18', null, '1');
INSERT INTO `article_tag` VALUES ('3', '日常', '2018-12-27 15:07:22', null, '1');
INSERT INTO `article_tag` VALUES ('5', '计划', '2018-12-28 09:34:16', null, '1');
INSERT INTO `article_tag` VALUES ('6', '提问', '2018-12-28 09:34:48', null, '1');
INSERT INTO `article_tag` VALUES ('7', '百科', '2018-12-28 09:35:01', null, '1');
INSERT INTO `article_tag` VALUES ('8', '图片', '2018-12-28 09:35:17', null, '1');
INSERT INTO `article_tag` VALUES ('9', '科技', '2018-12-28 09:35:25', null, '1');
INSERT INTO `article_tag` VALUES ('10', '体育', '2018-12-28 09:35:32', null, '1');
INSERT INTO `article_tag` VALUES ('11', '娱乐', '2018-12-28 09:35:42', null, '1');

-- ----------------------------
-- Table structure for article_type
-- ----------------------------
DROP TABLE IF EXISTS `article_type`;
CREATE TABLE `article_type` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `typeName` varchar(50) DEFAULT NULL,
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  `icon` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='文章类型';

-- ----------------------------
-- Records of article_type
-- ----------------------------
INSERT INTO `article_type` VALUES ('1', '随笔', '2018-12-27 14:24:27', null, '1', 'icon/post.png');
INSERT INTO `article_type` VALUES ('2', '短文', '2018-12-27 14:24:27', null, '1', 'icon/post.png');
INSERT INTO `article_type` VALUES ('3', '其他', '2018-12-27 14:24:27', null, '1', 'icon/post.png');

-- ----------------------------
-- Table structure for user_info
-- ----------------------------
DROP TABLE IF EXISTS `user_info`;
CREATE TABLE `user_info` (
  `id` bigint(20) NOT NULL AUTO_INCREMENT,
  `userName` varchar(255) DEFAULT NULL,
  `displayName` varchar(255) DEFAULT NULL,
  `loginPwd` varchar(255) DEFAULT NULL,
  `channel` varchar(255) DEFAULT NULL,
  `email` varchar(255) DEFAULT NULL,
  `roleCode` varchar(255) DEFAULT NULL,
  `levelId` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `faceImg` varchar(255) DEFAULT NULL,
  `createTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updateTime` datetime DEFAULT NULL,
  `validFlag` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='用户信息';

-- ----------------------------
-- Records of user_info
-- ----------------------------
INSERT INTO `user_info` VALUES ('1', 'monster', '恒', 'monster', 'default', 'monster2071@163.com', '', '1', '尔等', '/upload/image/2019-01-04/ca86739d74864a1a9b55e6163c505dc4.jpg', '2019-01-02 14:26:41', null, '1');
INSERT INTO `user_info` VALUES ('2', 'monster2071', 'monster2071', 'monster', 'default', 'monster@163.com', '', '1', null, null, '2019-01-02 17:04:42', null, '1');
